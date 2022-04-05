namespace HealthCare.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Behaviour.Filters;
    using BusinessLayer.Interfaces;
    using Contracts.Models.Common;
    using Contracts.Models.MedicalManAccount.Data;
    using Contracts.Models.MedicalManAccount.Requests;
    using Contracts.Models.MedicalManAccount.Responses;
    using Utilities.Enums;

    [ApiController]
    [ValidationFilter]
    [CustomAuthorizationFilter(RoleType.Admin, RoleType.Doctor)]
    [Route("medicalMan")]
    public class MedicalManController : ControllerBase
    {
        private readonly IMedicalManService _medicalManService;

        public MedicalManController(IMedicalManService medicalManService)
        {
            _medicalManService = medicalManService;
        }

        [Route("persistData")]
        [HttpPost]
        public async Task<TokenData> PersistData(PersistPersonalDataRequest request)
        {
            return await _medicalManService.PersistPersonalData(request);
        }

        [Route("retrieveData")]
        [HttpGet]
        public async Task<RetrieveMainDataResponse> RetrieveData()
        {
            return await _medicalManService.RetrievePersonalData();
        }

        [Route("retrieveContacts")]
        [HttpGet]
        public async Task<RetrieveContactsResponse> RetrieveContacts()
        {
            return await _medicalManService.RetrieveContacts();
        }

        [Route("getByMedCenterId/{centerId}")]
        [HttpGet]
        [DisableCustomAuthorizationFilter]
        public List<CommonMedicalData> GetByMedCenterId(int centerId)
        {
            return  _medicalManService.GetByMedicalCenter(centerId);
        }

        [Route("getStatistics")]
        [HttpGet]
        public StatisticsData GetStatistics()
        {
            return  _medicalManService.GetStatisticsData();
        }
    }
}