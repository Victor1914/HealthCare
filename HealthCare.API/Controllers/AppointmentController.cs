namespace HealthCare.API.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Behaviour.Filters;
    using BusinessLayer.Interfaces;
    using Contracts.Models.Appointment.Requests;
    using Contracts.Models.Appointment.Responses;
    using Contracts.Models.Common;
    using Utilities.Enums;

    [Route("appointment")]
    [ApiController]
    [CustomAuthorizationFilter(RoleType.Admin, RoleType.Patient, RoleType.Doctor)]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Route("request")]
        [HttpPost]
        [DisableCustomAuthorizationFilter]
        public async Task RequestAppointment(RequestAppointmentRequest request)
        {
            await _appointmentService.RequestAppointment(request);
        }

        [Route("changeStatus")]
        [HttpPost]
        public async Task<TokenData> ChangeAppointmentStatus(ChangeAppointmentStatusRequest request)
        {
            return await _appointmentService.ChangeAppointmentStatus(request);
        }

        [Route("getDoctorAppointments")]
        [HttpGet]
        public GetAppointmentsResponse GetAppointmentsForDoctor()
        {
            return  _appointmentService.GetAppointmentsForDoctor();
        }

        [Route("getPatientAppointments/{patientId}")]
        [HttpGet]
        public GetAppointmentsResponse GetPatientAppointmentsByDoctor(int patientId)
        {
            return _appointmentService.GetAppointmentsForPatientsByDoctor(patientId);
        }

        [Route("getAvailableHours/{medicalCenterId}/{medManId}")]
        [HttpGet]
        [DisableCustomAuthorizationFilter]
        public async Task<List<HourData>> GetAvailableHours(int medicalCenterId, int medManId)
        {
            return await _appointmentService.GetAvailableHours(medicalCenterId, medManId);
        }

        [Route("getAvailableHours/{medicalCenterId}")]
        [HttpGet]
        public async Task<List<HourData>> GetAvailableHoursAuth(int medicalCenterId)
        {
            return await _appointmentService.GetAvailableHoursAuth(medicalCenterId);
        }

        [Route("addAppointmentHours")]
        [HttpPost]
        public async Task<TokenData> AddAppointmentHours(AddAppointmentHoursRequest request)
        {
            return await _appointmentService.AddAppointmentHours(request);
        }

        [Route("getUpComingApps")]
        [HttpGet]
        public GetAppointmentsResponse GetUpComingApps()
        {
            return _appointmentService.GetUpComingAppointmentsForDoctor();
        }
    }
}