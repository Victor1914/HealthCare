namespace HealthCare.BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using Contracts.Interfaces;
    using Contracts.Models.Appointment.Data;
    using Contracts.Models.Appointment.Requests;
    using Contracts.Models.Appointment.Responses;
    using Contracts.Models.Common;

    using DataLayer;
    using DataLayer.Entities.MedicalMan;
    using DataLayer.Entities.UserAccount;
    using Exceptions;
    using Extensions;
    using Interfaces;
    using Utilities.Enums;
    using Utilities.Enums.Appointment;
    using Utilities.Enums.Common;
    using Utilities.Helpers;

    public class AppointmentService : IAppointmentService
    {
        private readonly HealthCareDbContext _dbContext;
        private readonly ISessionResolver _sessionResolver;
        private readonly IMapper _mapper;

        public AppointmentService(HealthCareDbContext dbContext, ISessionResolver sessionResolver, IMapper mapper)
        {
            _dbContext = dbContext;
            _sessionResolver = sessionResolver;
            _mapper = mapper;
        }

        public async Task RequestAppointment(RequestAppointmentRequest request)
        {
            var doctorInfo = _dbContext.MedicalManInfos.SingleOrDefault(x => x.Id == request.MedicalManInfoId);
            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => doctorInfo == null);

            var appointmentHour = _dbContext.AppointmentHours
                .SingleOrDefault(x => x.Id == request.AppointmentHourId 
                                      && x.AppointmentHourStatus == AppointmentHourStatus.Free
                                      && !x.IsDeleted);

            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => appointmentHour == null || request.AppointmentDate < DateTime.UtcNow);
            
            var patient = _dbContext.PatientInfos.SingleOrDefault(patientInfo => patientInfo.EGN == request.IdentityNumber);
            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => patient == null);

            var newAppointment = new Appointment
            {
                PatientId = patient.UserId,
                DoctorId = doctorInfo.UserId,
                AppointmentDate = request.AppointmentDate,
                Status = AppointmentStatus.Pending
            };

            appointmentHour.AppointmentHourStatus = AppointmentHourStatus.Taken;

            _dbContext.PersistModel(newAppointment, DatabaseOperation.Insert);
            _dbContext.PersistModel(appointmentHour, DatabaseOperation.Update);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<TokenData> ChangeAppointmentStatus(ChangeAppointmentStatusRequest request)
        {
            var appointment = _dbContext.Appointments.SingleOrDefault(x => x.Id == request.Id);
            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => appointment == null);

            appointment.Status = request.Status;

            _dbContext.PersistModel(appointment, DatabaseOperation.Update);

            await _dbContext.SaveChangesAsync();

            return new TokenData
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }

        public GetAppointmentsResponse GetAppointmentsForDoctor()
        {
            var userId = _sessionResolver.SessionInfo.UserId;
            var appointmentsData = _dbContext.Appointments
                .Where(app => app.DoctorId == userId)
                .Include(app => app.Patient.UserContact)
                .OrderByDescending(app => app.AppointmentDate)
                .ToList();

            var appointments = new List<AppointmentInfo>();
            foreach (var appointmentData in appointmentsData)
            {
                var appInfo = new AppointmentInfo
                {
                    Id = appointmentData.Id,
                    Status = appointmentData.Status,
                    AppointmentHour = appointmentData.AppointmentDate,
                    PatientInfo = new PatientInfoData
                    {
                        PatientInfoId = appointmentData.PatientId
                    }
                };

                var contacts = appointmentData.Patient.UserContact;
                if (contacts.Addresses.Any())
                    appInfo.PatientInfo.Email = contacts.Addresses[0].StreetAddress;

                if (contacts.Emails.Any())
                    appInfo.PatientInfo.Email = contacts.Emails[0].EmailAddress;

                if (contacts.Phones.Any())
                    appInfo.PatientInfo.Email = contacts.Phones[0].Number;

                var patientInfo = _dbContext.PatientInfos.SingleOrDefault(profile => profile.UserId == appointmentData.Patient.Id);
                appInfo.PatientInfo.Gender = patientInfo?.Gender ?? Gender.None;
                appInfo.PatientInfo.Name = patientInfo?.Name;

                appointments.Add(appInfo);
            }

            return new GetAppointmentsResponse
            {
                Appointments = appointments,
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }

        public GetAppointmentsResponse GetUpComingAppointmentsForDoctor()
        {
            var userId = _sessionResolver.SessionInfo.UserId;
            var appointmentsData = _dbContext.Appointments
                .Where(app => app.DoctorId == userId                 
                              && app.AppointmentDate > DateTime.UtcNow                 
                              && !app.IsDeleted                               
&& app.Status == AppointmentStatus.Approved)
                .OrderByDescending(app => app.AppointmentDate)
                .Include(app => app.Patient.UserContact)
                .ToList();

            var appointments = new List<AppointmentInfo>();
            foreach (var appointmentData in appointmentsData)
            {
                var appInfo = new AppointmentInfo
                {
                    Id = appointmentData.Id,
                    Status = appointmentData.Status,
                    AppointmentHour = appointmentData.AppointmentDate,
                    PatientInfo = new PatientInfoData
                    {
                        PatientInfoId = appointmentData.PatientId
                    },
                    Reason = appointmentData.Reason.ToString()
                };

                var contacts = appointmentData.Patient.UserContact;
                if (contacts.Addresses.Any())
                    appInfo.PatientInfo.Email = contacts.Addresses[0].StreetAddress;

                if (contacts.Emails.Any())
                    appInfo.PatientInfo.Email = contacts.Emails[0].EmailAddress;

                if (contacts.Phones.Any())
                    appInfo.PatientInfo.Email = contacts.Phones[0].Number;

                var patientInfo = _dbContext.PatientInfos.SingleOrDefault(profile => profile.UserId == appointmentData.Patient.Id);
                appInfo.PatientInfo.Gender = patientInfo?.Gender ?? Gender.None;
                appInfo.PatientInfo.Name = patientInfo?.Name;

                appointments.Add(appInfo);
            }

            return new GetAppointmentsResponse
            {
                Appointments = appointments,
                Token = _sessionResolver.SessionInfo.NewToken,
            };
        }

        public GetAppointmentsResponse GetAppointmentsForPatientsByDoctor(int patientId)
        {
            // patientId e userId на пациента
            var userId = _sessionResolver.SessionInfo.UserId;
            var appointmentsData = _dbContext.Appointments
                .Where(app => app.DoctorId == userId && app.PatientId == patientId)
                .ToList();

            var appointments = appointmentsData
                .Select(appointment =>
                    new AppointmentInfo
                    {
                        Id = appointment.Id,
                        Status = appointment.Status,
                        AppointmentHour = appointment.AppointmentDate,
                        PatientInfo = _mapper.Map<PatientInfoData>(appointmentsData)
                    })
                .ToList();

            return new GetAppointmentsResponse
            {
                Appointments = appointments,
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }


        public async Task<TokenData> AddAppointmentHours(AddAppointmentHoursRequest request)
        {
            var medicalManInfoId = _dbContext.MedicalManInfos
                .SingleOrDefault(x => x.UserId == _sessionResolver.SessionInfo.UserId)?.Id;
            
            var medicalCenterInfo = _dbContext.MedicalCenterInfos.SingleOrDefault(x => x.UserId == request.MedicalCenterId);
            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => medicalCenterInfo == null);
            
            foreach (var hour in request.Hours)
            {
                if(hour.HourStatus == HourStatus.NotModified)
                    continue;
                
                AppointmentHours appointmentHour;
                switch (hour.HourStatus)
                {
                    case HourStatus.Deleted:
                        appointmentHour = _dbContext.AppointmentHours.SingleOrDefault(app => app.Id == hour.Id);
                        ValidationUtils.ValidateAndThrow<DataMismatchException>(() => appointmentHour == null);

                        _dbContext.DeleteEntity(appointmentHour);

                        continue;
                    case HourStatus.Updated:
                        appointmentHour = _dbContext.AppointmentHours.SingleOrDefault(app => app.Id == hour.Id);
                        ValidationUtils.ValidateAndThrow<DataMismatchException>(() => appointmentHour == null);

                        appointmentHour.StartDate = hour.StartDate;
                        appointmentHour.EndDate = hour.EndDate;
                        break;
                    default:
                        appointmentHour = new AppointmentHours
                        {
                            StartDate = hour.StartDate,
                            EndDate = hour.EndDate,
                            MedicalManInfoId = medicalManInfoId.GetValueOrDefault(),
                            MedicalCenterInfoId = medicalCenterInfo.Id
                        };
                        break;
                }

                _dbContext.PersistModel(appointmentHour, appointmentHour.Id == 0 ? DatabaseOperation.Insert : DatabaseOperation.Update);
            }

            await _dbContext.SaveChangesAsync();

            return new TokenData
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        } 

        public async Task<List<HourData>> GetAvailableHoursAuth(int medCenterId)
        {
            var medManInfo = _dbContext.MedicalManInfos.FirstOrDefault(medMan => medMan.UserId == _sessionResolver.SessionInfo.UserId);

            return await GetAvailableHours(medCenterId, medManInfo.Id);
        }

        public async Task<List<HourData>> GetAvailableHours(int medCenterId, int medManId)
        {
            var medCenterInfo = await _dbContext.MedicalCenterInfos
                .SingleOrDefaultAsync(medCenter => medCenter.UserId == medCenterId);
            
            var medManInfo = await _dbContext.MedicalManInfos
                .SingleOrDefaultAsync(medMan => medMan.Id == medManId);

            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => medManInfo == null || medCenterInfo == null);

            var hoursRaw = _dbContext.AppointmentHours
                .Where(hour => hour.MedicalCenterInfoId == medCenterInfo.Id 
                               && hour.MedicalManInfoId == medManInfo.Id
                               && !hour.IsDeleted
                               && hour.AppointmentHourStatus == AppointmentHourStatus.Free
                               && hour.StartDate.ToUniversalTime() > DateTime.UtcNow)
                .OrderBy(hour => hour.StartDate)
                .ToList();
            
            return _mapper.Map<List<HourData>>(hoursRaw);
        }
    }
}
