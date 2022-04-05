namespace HealthCare.BusinessLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;

    using Contracts.Interfaces;
    using Contracts.Models.Common;
    using Contracts.Models.OutpatientCard;
    using Contracts.Models.OutpatientCard.Data;
    using Contracts.Models.OutpatientCard.Requests;
    using Contracts.Models.OutpatientCard.Responses;
    using DataLayer;
    using DataLayer.Entities.MedicalData;
    using DataLayer.Entities.OutpatientCardData;
    using Exceptions;
    using Extensions;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Utilities.Enums;
    using Utilities.Helpers;

    public class OutPatientCardService : IOutPatientCardService
    {
        private readonly HealthCareDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISessionResolver _sessionResolver;
        private readonly IMedicalDataService _medicalDataService;

        public OutPatientCardService(HealthCareDbContext dbContext, IMapper mapper, ISessionResolver sessionResolver, IMedicalDataService medicalDataService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sessionResolver = sessionResolver;
            _medicalDataService = medicalDataService;
        }

        public async Task<TokenData> Add(AddOutpatientCardRequest request, IList<IFormFile> files)
        {
            var outpatientCard = request.OutPatientCardInfo;

            var patient = _dbContext.PatientInfos.SingleOrDefault(info => info.Id == outpatientCard.PatientId);
            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => patient == null);

            var dbModel = _mapper.Map<OutpatientCard>(outpatientCard);

            dbModel.DoctorId = _sessionResolver.SessionInfo.UserId;
            dbModel.PatientId = patient.UserId;

            var dbOperation = dbModel.Id.GetDbOperation();
            var outpatientCardId = _dbContext.PersistModel(dbModel, dbOperation);

           _medicalDataService.PersistMedicalDataRelatedEntity<MainIllnessData, Illness>(outpatientCard.MainDiagnose, outpatientCardId, DocumentType.OutpatientCard);
           _medicalDataService.PersistMedicalDataRelatedEntities<OutPatientCardIllnessData, Illness>(outpatientCard.OtherDiseases.EmptyIfNull(), outpatientCardId, DocumentType.OutpatientCard);
           _medicalDataService.PersistMedicalDataRelatedEntities<MedicalDocuments, ConsultationDocument>(outpatientCard.MedicalDocuments.EmptyIfNull(), outpatientCardId, DocumentType.OutpatientCard);
           _medicalDataService.PersistMedicalDataRelatedEntities<MedicalDocumentsMMD, ConsultationDocumentMMD>(outpatientCard.MedicalDocumentsMMD.EmptyIfNull(), outpatientCardId, DocumentType.OutpatientCard);
           _medicalDataService.PersistMedicalDataRelatedEntities<MedicalDocumentsVSD, ConsultationDocument>(outpatientCard.MedicalDocumentsVSD.EmptyIfNull(), outpatientCardId, DocumentType.OutpatientCard);
           _medicalDataService.PersistMedicalDataRelatedEntity<MedicalDocumentsKP, ConsultationDocumentKP>(outpatientCard.MedicalDocumentsKP, outpatientCardId, DocumentType.OutpatientCard);
           _medicalDataService.PersistMedicalDataRelatedEntity<SickLeaveData, SickLeave>(outpatientCard.SickLeave, outpatientCardId, DocumentType.OutpatientCard);
          
            if(files.Any())
                _medicalDataService.PersistMedicalTests(files, outpatientCardId, DocumentType.OutpatientCard);

            await _dbContext.SaveChangesAsync();

            return new TokenData
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }

        public GetOutPatientCardsResponse GetAllByPatient(int patientInfoId)
        {
            var patientInfo = _dbContext.PatientInfos.SingleOrDefault(info => info.Id == patientInfoId);

            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => patientInfo == null);

            var outPatientCardDbModels = _dbContext.OutpatientCards
                .Where(card => card.Patient.Id == patientInfo.UserId)
                .Include(card => card.Doctor.MedicalManInfo)
                .Include(card => card.MedCenter.MedicalCenterInfo)
                .Include(card => card.ConsultationDocuments)
                .Include(card => card.OutpatientCardIllnesses)
                .ThenInclude(illness => illness.IllnessType)
                .Include(card => card.ConsultationDocumentsMMD)
                .Include(card => card.OutpatientCardMedicalTests)
                .OrderBy(card => card.CreateDate)
                .ToList();


            var results = _mapper.Map<List<OutPatientCardInfo>>(outPatientCardDbModels);

            foreach (var result in results)
            {
                var sickLeave = _dbContext.SickLeaves.SingleOrDefault(sl => sl.OutpatientCard.Id == result.Id);
                result.SickLeave = _mapper.Map<SickLeaveData>(sickLeave);
                
                var kp = _dbContext.ConsultationDocumentsKP.SingleOrDefault(sl => sl.OutpatientCard.Id == result.Id);
                result.MedicalDocumentsKP = _mapper.Map<MedicalDocumentsKP>(kp);
            }

            return new GetOutPatientCardsResponse
            {
                Token = _sessionResolver.SessionInfo.NewToken,
                OutPatientCardInfo = results
            };
        }
    }
}
