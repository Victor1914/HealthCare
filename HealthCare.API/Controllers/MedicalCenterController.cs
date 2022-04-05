namespace HealthCare.API.Controllers
{
    using System.Threading.Tasks;
    using Behaviour.Filters;
    using Microsoft.AspNetCore.Mvc;

    using BusinessLayer.Interfaces;
    using Contracts.Models.Common;
    using Contracts.Models.MedicalCenterAccount.Requests;
    using Contracts.Models.MedicalCenterAccount.Responses;
    using Utilities.Enums;

    [ApiController]
    [ValidationFilter]
    [CustomAuthorizationFilter(RoleType.Admin, RoleType.Doctor, RoleType.MedicalCenter)]
    [Route("medicalCenter")]
    public class MedicalCenterController : ControllerBase
    {
        private readonly IMedicalCenterService _medicalCenterService;

        public MedicalCenterController(IMedicalCenterService medicalCenterService)
        {
            _medicalCenterService = medicalCenterService;
        }

        [Route("persistMedicalCenterData")]
        [HttpPost]
        public async Task<TokenData> PersistMedicalCenterData(PersistMedicalCenterDataRequest request)
        {
            return await _medicalCenterService.PersistMedicalCenterData(request);
        }

        [Route("getMedCenters/{cityId?}")]
        [HttpGet]
        [DisableCustomAuthorizationFilter]
        public GetMedicalCentersResponse GetByCity(int? cityId = null)
        {
            return _medicalCenterService.GetMedicalCenters(cityId);
        }

        [Route("getMedCentersForDoctor")]
        [HttpGet]
        public async Task<GetMedicalCentersResponse> GetForDoctor()
        {
            return await _medicalCenterService.GetMedicalCentersForDoctor();
        }
    }
}   