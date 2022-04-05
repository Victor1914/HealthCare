namespace HealthCare.BusinessLayer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Contracts.Interfaces;
    using Contracts.Models.Common;
    using Contracts.Models.MedicalCenterAccount.Data;
    using Contracts.Models.MedicalCenterAccount.Requests;
    using Contracts.Models.MedicalCenterAccount.Responses;

    using DataLayer;
    using Exceptions;
    using Extensions;
    using Interfaces;
    using Utilities.Enums;
    using Utilities.Enums.Common;
    using Utilities.Helpers;

    public class MedicalCenterService : IMedicalCenterService
    {
        private readonly ISessionResolver _sessionResolver;
        private readonly IMapper _mapper;
        private readonly HealthCareDbContext _dbContext;

        public MedicalCenterService(ISessionResolver sessionResolver, HealthCareDbContext dbContext, IMapper mapper)
        {
            _sessionResolver = sessionResolver;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TokenData> PersistMedicalCenterData(PersistMedicalCenterDataRequest request)
        {
            var medicalCenterInfo = _dbContext.MedicalCenterInfos.SingleOrDefault(x => x.UserId == _sessionResolver.SessionInfo.UserId);
            ValidationUtils.ValidateAndThrow<DataMismatchException>(() => medicalCenterInfo == null);

            var dbModel = _mapper.Map(request.Data, medicalCenterInfo);

            _dbContext.PersistModel(dbModel, DatabaseOperation.Update);
            await _dbContext.SaveChangesAsync();

            return new TokenData
            {
                Token = _sessionResolver.SessionInfo.NewToken
            };
        }

        public GetMedicalCentersResponse GetMedicalCenters(int? cityId)
        {
            var medicalCentersRawData = (cityId != null
                ? _dbContext.MedicalCenterInfos
                    .Where(center => center.User.UserContact.Addresses[0].CityId == cityId)
                : _dbContext.MedicalCenterInfos)
                    .ToList();

            var medicalCenters = _mapper.Map<List<MedicalCenterData>>(medicalCentersRawData);

            return new GetMedicalCentersResponse
            {
                MedicalCenters = medicalCenters
            };
        }

        public async Task<GetMedicalCentersResponse> GetMedicalCentersForDoctor()
        {
            var userId = _sessionResolver.SessionInfo.UserId;
            var workPlaces = _dbContext.MedicalCenterDoctors
                .Where(place => place.DoctorId == userId && !place.IsDeleted)
                .ToList();

            var medicalCenters = new List<MedicalCenterData>();
            foreach (var workPlace in workPlaces)
            {
                var medCenterInfo = _dbContext.MedicalCenterInfos
                    .SingleOrDefault(medCenter => medCenter.UserId == workPlace.MedicalCenterId);

                var medicalCenterData = new MedicalCenterData
                {
                    Id = medCenterInfo.UserId,
                    Name = medCenterInfo.Name,
                };

                medicalCenters.Add(medicalCenterData);
            }

            return new GetMedicalCentersResponse
            {
                Token = _sessionResolver.SessionInfo.NewToken,
                MedicalCenters = medicalCenters
            };
        }

        public Task<GetMedicalCentersResponse> GetDoctorForMedicalCenter()
        {
            throw new System.NotImplementedException();
        }
    }
}
