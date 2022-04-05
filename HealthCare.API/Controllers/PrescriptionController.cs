namespace HealthCare.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Behaviour.Filters;
    using BusinessLayer.Interfaces;
    using Contracts.Models.Common;
    using Contracts.Models.Prescription.Requests;
    using Contracts.Models.Prescription.Responses;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Enums;

    [ApiController]
    [ValidationFilter]
    [Route("prescription")]
    [CustomAuthorizationFilter(RoleType.Admin, RoleType.Patient, RoleType.Doctor)]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [Route("add")]
        [HttpPost]
        public async Task<TokenData> AddPrescription(AddPrescriptionRequest prescription)
        {
           return await _prescriptionService.AddPrescription(prescription);
        }

        [Route("getAll/{patientId}")]
        [HttpGet]
        public async Task<GetPrescriptionsResponse> GetAllPrescription(int patientId)
        {
            return await _prescriptionService.GetPatientPrescriptions(patientId);
        }
    }
}
