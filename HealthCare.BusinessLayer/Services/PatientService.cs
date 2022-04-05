namespace HealthCare.BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Contracts.Interfaces;
    using Contracts.Models.Appointment.Responses;
    using Contracts.Models.Common;
    using Contracts.Models.CommonMedicalData;
    using Contracts.Models.OutpatientCard.Data;
    using Contracts.Models.OutpatientCard.Responses;
    using Contracts.Models.PatientAccount.Data;
    using Contracts.Models.PatientAccount.Requests;
    using Contracts.Models.PatientAccount.Responses;

    using DataLayer;
    using DataLayer.Entities.MedicalData;

    using Exceptions;

    using Extensions;

    using Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    using Utilities.Enums;
    using Utilities.Enums.Common;
    using Utilities.Helpers;

    public class PatientService : IPatientService
    {
        private readonly IMapper _mapper;
        private readonly HealthCareDbContext _dbContext;
        private readonly ISessionResolver _sessionResolver;
        private readonly IMedicalDataService _medicalDataService;

        public PatientService(IMapper mapper,
            HealthCareDbContext dbContext,
            ISessionResolver sessionResolver,
            IMedicalDataService medicalDataService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _sessionResolver = sessionResolver;
            _medicalDataService = medicalDataService;
        }

        public async Task<TokenData> PersistMedicalProfile(PersistMedicalProfileRequest request)
        {
            var patientInfoId = PersistPatientPersonalData(request.PatientData);
            var medicalProfileId = PersistMedicalProfileData(request.MedicalData, patientInfoId);

            _medicalDataService.PersistMedicalDataRelatedEntities<IllnessData, Illness>(request.Illnesses.EmptyIfNull(), medicalProfileId, DocumentType.MedicalProfile);

            await _dbContext.SaveChangesAsync();

            return new TokenData
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }

        public async Task<GetPatientByEgnResponse> GetPatientByEng(long egn)
        {
            var patientDb = await _dbContext.PatientInfos
                .Where(x => x.EGN == egn)
                .Include(x => x.Patient)
                .ThenInclude(pat => pat.UserContact.Emails)
                .ThenInclude(pat => pat.UserContact.Phones)
                .ThenInclude(pat => pat.UserContact.Addresses)
                .ThenInclude(add => add.City)
                .SingleOrDefaultAsync();

            var patient = _mapper.Map<PatientInfoData>(patientDb);

            return new GetPatientByEgnResponse
            {
                Patient = patient
            };
        }

        public async Task<GetPatientAppointmentsResponse> GetAppointments(int patientInfoId)
        {
            var patientInfo = await _dbContext.PatientInfos.SingleOrDefaultAsync(patient => patient.Id == patientInfoId);
            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => patientInfo == null);

            var appointments = _dbContext.Appointments
                .Where(app => app.PatientId == patientInfo.UserId)
                .Include(app => app.Doctor)
                .ThenInclude(doc => doc.MedicalManInfo)
                .ThenInclude(info => info.Specialties)
                .ThenInclude(spec => spec.Specialty)
                .Include(app => app.Patient)
                .OrderByDescending(app => app.AppointmentDate)
                .ToList();

            var results = new List<PatientAppointment>();

            foreach (var app in appointments)
            {
                var result = new PatientAppointment
                {
                    Id = app.Id,
                    AppointmentHour = app.AppointmentDate,
                    Status = app.Status,
                    DoctorName = app.Doctor.MedicalManInfo.Name,
                    Specialty = app.Doctor.MedicalManInfo.Specialties[0].Specialty.Name,
                    IsCurrentDoctor = app.Doctor.Id == _sessionResolver.SessionInfo.UserId,
                };

                results.Add(result);
            }

            return new GetPatientAppointmentsResponse
            {
                Token = _sessionResolver.SessionInfo.NewToken,
                Appointments = results
            };
        }

        private int PersistPatientPersonalData(PatientGeneralData requestPatientData)
        {
            var patientInfo = _dbContext.PatientInfos.SingleOrDefault(x => x.UserId == _sessionResolver.SessionInfo.UserId);

            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => patientInfo == null);

            var dbModel = _mapper.Map(requestPatientData, patientInfo);
            dbModel.UserId = _sessionResolver.SessionInfo.UserId;

            return _dbContext.PersistModel(dbModel, DatabaseOperation.Update);
        }

        private int PersistMedicalProfileData(MedicalProfileGeneralData data, int patientInfoId)
        {
            var dbModel = _dbContext.MedicalProfiles.SingleOrNew(x => x.PatientInfoId == patientInfoId);

            dbModel = _mapper.Map(data, dbModel);
            dbModel.PatientInfoId = patientInfoId;

            var dbOperation = dbModel.Id.GetDbOperation();
            var medicalProfileId = _dbContext.PersistModel(dbModel, dbOperation);

            return medicalProfileId;
        }
    }
}