namespace HealthCare.BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Contracts.Models.Common;

    using DataLayer;
    using DataLayer.Entities.MedicalData;
    using DataLayer.Entities.MedicalMan;
    using DataLayer.Entities.UserAccount.Contacts;

    using Interfaces;

    using Utilities.Enums.Visitation;

    public class CommonService : ICommonService
    {
        private readonly HealthCareDbContext _dbContext;
        private readonly IMapper _mapper;

        public CommonService(HealthCareDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<CityData> GetCities()
            => GetEntities<CityData, City>();

        public List<SpecialtyData> GetSpecialties()
            => GetEntities<SpecialtyData, Specialty>();

        public List<CommonMedicalData> GetIllnesses(int categoryId)
        {
            var rawData = _dbContext.IllnessTypes
                .Where(illness => illness.IllnessCategoryId == categoryId)
                .ToList();

            return _mapper.Map<List<CommonMedicalData>>(rawData);
        }

        public List<CommonMedicalData> GetIllnessCategories()
            => GetEntities<CommonMedicalData, IllnessCategory>();

        public List<VisitationDetailsData> GetVisitationReasons()
        {
            return Enum.GetNames(typeof(VisitationReason))
                .Select(name => new VisitationDetailsData
                {
                    Id = (int)Enum.Parse(typeof(VisitationReason), name),
                    Name = name
                })
                .ToList();
        }

        public List<CommonMedicalData> GetVisitationTypes()
        {
            return Enum.GetNames(typeof(VisitationType))
                .Select(name => new CommonMedicalData
                {
                    Id = (int)Enum.Parse(typeof(VisitationType), name),
                    Name = name
                })
                .ToList();
        }

        private List<TModel> GetEntities<TModel, TSet>() where TSet : class
        {
            var rawData = _dbContext.Set<TSet>().ToList();

            return _mapper.Map<List<TModel>>(rawData);
        }
    }
}
