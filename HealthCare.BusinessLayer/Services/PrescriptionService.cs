namespace HealthCare.BusinessLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Contracts.Interfaces;
    using Contracts.Models.Common;
    using Contracts.Models.Prescription.Requests;
    using Contracts.Models.Prescription.Responses;

    using DataLayer;
    using DataLayer.Entities.MedicalData;

    using Exceptions;

    using Extensions;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;
    
    using Utilities.Enums.Common;
    using Utilities.Helpers;

    public class PrescriptionService : IPrescriptionService
    {
        private readonly HealthCareDbContext _dbContext;
        private readonly ISessionResolver _sessionResolver;
        private readonly IMapper _mapper;

        public PrescriptionService(HealthCareDbContext dbContext, ISessionResolver sessionResolver, IMapper mapper)
        {
            _dbContext = dbContext;
            _sessionResolver = sessionResolver;
            _mapper = mapper;
        }

        public async Task<TokenData> AddPrescription(AddPrescriptionRequest prescription)
        {
            var patient = _dbContext.PatientInfos.SingleOrDefault(info => info.Id == prescription.PrescriptionData.PatientId);

            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => patient == null);
            
            var dbModel = _mapper.Map<Prescription>(prescription.PrescriptionData);

            dbModel.PatientId = patient.UserId;
            dbModel.DoctorId = _sessionResolver.SessionInfo.UserId;

            _dbContext.PersistModel(dbModel, DatabaseOperation.Insert);

            await _dbContext.SaveChangesAsync();

            return new TokenData
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }

        public async Task<GetPrescriptionsResponse> GetPatientPrescriptions(int patientInfoId)
        {
            var patient = _dbContext.PatientInfos.SingleOrDefault(info => info.Id == patientInfoId);
            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => patient == null);

            var prescriptions = _dbContext.Prescriptions
                .Where(pres => pres.PatientId == patient.UserId)
                .Include(pres => pres.Doctor)
                .ThenInclude(doctor => doctor.MedicalManInfo)
                .ThenInclude(docSpec => docSpec.Specialties)
                .ThenInclude(spec => spec.Specialty)
                .OrderByDescending(pres => pres.CreateDate)
                .ToList();

            var result = _mapper.Map<List<ExtendedPrescriptionData>>(prescriptions);

            return new GetPrescriptionsResponse
            {
                Token = _sessionResolver.SessionInfo.NewToken,
                Prescriptions = result
            };
        }
    }
}
