namespace HealthCare.API.Controllers
{
    using System.Collections.Generic;
    using BusinessLayer.Interfaces;
    using Contracts.Models.Common;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService cityService)
        {
            _commonService = cityService;
        }

        [Route("getCities")]
        [HttpGet]
        public List<CityData> GetCities()
        {
            return  _commonService.GetCities();
        }

        [Route("getSpecialties")]
        [HttpGet]
        public List<SpecialtyData> GetSpecialties()
        {
            return  _commonService.GetSpecialties();
        }

        [Route("getIllnessCategories")]
        [HttpGet]
        public List<CommonMedicalData> GetIllnessCategories()
        {
            return  _commonService.GetIllnessCategories();
        }

        [Route("getIllnesses/{categoryId}")]
        [HttpGet]
        public List<CommonMedicalData> GetIllnesses(int categoryId)
        {
            return  _commonService.GetIllnesses(categoryId);
        }

        [Route("getVisitationReasons")]
        [HttpGet]
        public List<VisitationDetailsData> GetVisitationReason()
        {
            return  _commonService.GetVisitationReasons();
        }

        [Route("getVisitationTypes")]
        [HttpGet]
        public List<CommonMedicalData> GetVisitationTypes()
        {
            return  _commonService.GetVisitationTypes();
        }
    }
}
