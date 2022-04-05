namespace HealthCare.Contracts.Models.Appointment.Requests
{
    using Utilities.Enums.Appointment;

    public class ChangeAppointmentStatusRequest
    {
        public int Id { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}
