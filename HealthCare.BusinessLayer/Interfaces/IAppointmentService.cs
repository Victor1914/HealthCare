namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Contracts.Models.Appointment.Requests;
    using Contracts.Models.Appointment.Responses;
    using Contracts.Models.Common;

    public interface IAppointmentService
    {
        Task RequestAppointment(RequestAppointmentRequest request);

        Task<TokenData> ChangeAppointmentStatus(ChangeAppointmentStatusRequest request);

        GetAppointmentsResponse GetAppointmentsForDoctor();

        Task<List<HourData>> GetAvailableHours(int medCenterId, int medManId);

        Task<List<HourData>> GetAvailableHoursAuth(int medCenterId);

        GetAppointmentsResponse GetAppointmentsForPatientsByDoctor(int patientId);

        Task<TokenData> AddAppointmentHours(AddAppointmentHoursRequest request);

        GetAppointmentsResponse GetUpComingAppointmentsForDoctor();
    }
}
