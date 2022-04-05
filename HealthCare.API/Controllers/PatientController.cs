namespace HealthCare.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using Behaviour.Filters;
    using BusinessLayer.Interfaces;
    using Contracts.Models.Appointment.Responses;
    using Contracts.Models.Common;
    using Contracts.Models.PatientAccount.Requests;
    using Contracts.Models.PatientAccount.Responses;
    using Utilities.Enums;

    [ApiController]
    [ValidationFilter]
    [Route("patient")]
    [CustomAuthorizationFilter(RoleType.Admin, RoleType.Patient, RoleType.Doctor)]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [Route("persistMedicalProfile")]
        [HttpPost]
        public async Task<TokenData> PersistMedicalProfile(PersistMedicalProfileRequest request)
        {
            return await _patientService.PersistMedicalProfile(request);
        }

        [Route("getByEgn/{egn}")]
        [HttpGet]
        [DisableCustomAuthorizationFilter]
        public async Task<GetPatientByEgnResponse> GetPatientByEgn(long egn)
        {
            return await _patientService.GetPatientByEng(egn);
        }

        [Route("getPatientAppointments/{patientId}")]
        [HttpGet]
        public async Task<GetPatientAppointmentsResponse> GetAppointments(int patientId)
        {
            return await _patientService.GetAppointments(patientId);
        }
    }
}
