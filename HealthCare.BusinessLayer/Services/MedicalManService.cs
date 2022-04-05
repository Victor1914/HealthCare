namespace HealthCare.BusinessLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;

    using Contracts.Interfaces;
    using Contracts.Models.Common;
    using Contracts.Models.MedicalCenterAccount.Data;
    using Contracts.Models.MedicalManAccount.Data;
    using Contracts.Models.MedicalManAccount.Requests;
    using Contracts.Models.MedicalManAccount.Responses;
    using Contracts.Models.UserAccount.Data;

    using DataLayer;
    using DataLayer.Entities.UserAccount.Contacts;
    using Exceptions;
    using Extensions;
    using Interfaces;
    using Utilities.Enums.Common;
    using Utilities.Helpers;

    public class MedicalManService : IMedicalManService
    {
        private readonly HealthCareDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISessionResolver _sessionResolver;

        public MedicalManService(HealthCareDbContext dbContext, IMapper mapper, ISessionResolver sessionResolver)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sessionResolver = sessionResolver;
        }

        public async Task<TokenData> PersistPersonalData(PersistPersonalDataRequest request)
        {
            var userId = _sessionResolver.SessionInfo.UserId;
            var user = _dbContext.Users.SingleOrDefault(usr => usr.Id == userId);
            var medicalManInfo = _dbContext.MedicalManInfos.SingleOrDefault(x => x.UserId == userId);

            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => medicalManInfo == null);

            var dbModel = _mapper.Map(request.GeneralDataData, medicalManInfo);
            dbModel.UserId = userId;

            var medicalManInfoId = _dbContext.PersistModel(dbModel, DatabaseOperation.Update);

            var medicalManCurrentSpecialties = _dbContext.MedicalManSpecialties
                .Where(x => x.MedManInfoId == medicalManInfoId && !x.IsDeleted)
                .ToList();

            var medicalManCurrentWorkPlaces = _dbContext.MedicalCenterDoctors
                .Where(x => x.DoctorId == userId && !x.IsDeleted)
                .ToList();

            dbModel.AddSpecialties(request.Specialties.EmptyIfNull(), medicalManCurrentSpecialties, medicalManInfoId);
            user.AddMedicalCenters(request.MedicalCenters.EmptyIfNull(), medicalManCurrentWorkPlaces, userId);

            await _dbContext.SaveChangesAsync();

            return new TokenData
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }

        public async Task<RetrieveMainDataResponse> RetrievePersonalData()
        {
            var user =  await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == _sessionResolver.SessionInfo.UserId);
            var medicalManInfo = await _dbContext.MedicalManInfos.SingleOrDefaultAsync(info => info.UserId == user.Id);

            var response = new RetrieveMainDataResponse();

            var generalData = new MedicalManGeneralData
            {
                Name = medicalManInfo.Name ?? string.Empty,
                Gender = medicalManInfo.Gender,
                BirthDate = medicalManInfo.BirthDate.ToShortDateString().NullIfDefault()?.ToString(),
                IdentityNumber = medicalManInfo.IdentityNumber
            };

            var specialties = _dbContext.MedicalManSpecialties
                .Where(spec => spec.MedManInfoId == medicalManInfo.Id && !spec.IsDeleted)
                .ToList();

            if (specialties.Any())
            {
                response.Specializations = specialties
                    .Select(spec => new SpecialtyData
                    {
                        Id = spec.SpecialtyId,
                        Name = _dbContext.Specialties.SingleOrDefault(spc => spc.Id == spec.SpecialtyId).Name,
                        Code = _dbContext.Specialties.SingleOrDefault(spc => spc.Id == spec.SpecialtyId).Code
                    })
                    .ToList();
            }

            var workPlaces = _dbContext.MedicalCenterDoctors
                .Where(place => place.DoctorId == user.Id && !place.IsDeleted)
                .Include(place => place.MedicalCenter.UserContact.Addresses)
                .ThenInclude(addr => addr.City)
                .ToList();

            foreach (var workPlace in workPlaces)
            {
                var medCenterInfo = _dbContext.MedicalCenterInfos
                    .SingleOrDefault(medCenter => medCenter.UserId == workPlace.MedicalCenterId);

                var workLocation = new WorkLocation
                {
                    Id = workPlace.Id,
                    City = new CityData
                    {
                        Id = workPlace.MedicalCenter.UserContact.Addresses[0].City.Id,
                        Name = workPlace.MedicalCenter.UserContact.Addresses[0].City.Name
                    },
                    MedicalCenter = new MedicalCenterData
                    {
                        Id = medCenterInfo.UserId,
                        Name = medCenterInfo.Name,
                    }
                };

                response.WorkLocations.Add(workLocation);
            }

            response.Token = _sessionResolver.SessionInfo.NewToken;
            response.GeneralData = generalData;
            response.Username = user.Username;

            return response;
        }

        public async Task<RetrieveContactsResponse> RetrieveContacts()
        {
            var user = _dbContext.Users
                .Where(u => u.Id == _sessionResolver.SessionInfo.UserId && !u.IsDeleted)
                .Include(u => u.UserContact.Emails)
                .Include(u => u.UserContact.Phones)
                .SingleOrDefault();

            var emails = _mapper.Map<List<Email>, List<EmailData>>(user.UserContact.Emails.EmptyIfNullToList()
                .Where(email => !email.IsDeleted).ToList());

            var phoneNumbers = _mapper.Map<List<Phone>, List<PhoneData>>(user.UserContact.Phones.EmptyIfNullToList()
                .Where(phone => !phone.IsDeleted).ToList());

            return new RetrieveContactsResponse
            {
                Token = _sessionResolver.SessionInfo.NewToken,
                Emails = emails,
                PhoneNumbers = phoneNumbers
            };
        }

        public List<CommonMedicalData> GetByMedicalCenter(int centerId)
        {
            var dbResult = _dbContext.Users
                .Where(user => user.DoctorWorkPlaces.Any(place => place.MedicalCenterId == centerId && !place.IsDeleted))
                .Include(user => user.MedicalManInfo)
                .Select(user => user.MedicalManInfo)
                .ToList();

            var map = _mapper.Map<List<CommonMedicalData>>(dbResult);

            return map;
        }

        public StatisticsData GetStatisticsData()
        {
            return new StatisticsData
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }
    }
}