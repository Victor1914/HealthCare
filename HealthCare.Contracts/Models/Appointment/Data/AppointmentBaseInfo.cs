namespace HealthCare.Contracts.Models.Appointment.Data
{
    using System;
    using Utilities.Enums.Appointment;

    public class AppointmentBaseInfo
    {
        public int Id {get;set;}

        public DateTime AppointmentHour { get;set; }

        public AppointmentStatus Status { get; set; }

        public string Reason { get; set; }
    }
}
