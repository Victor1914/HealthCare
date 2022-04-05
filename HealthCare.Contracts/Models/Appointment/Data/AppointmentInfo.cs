namespace HealthCare.Contracts.Models.Appointment.Data
{
    using Common;

    public class AppointmentInfo : AppointmentBaseInfo
    {
        public PatientInfoData PatientInfo { get; set; }
    }
}
