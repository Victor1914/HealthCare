namespace HealthCare.Templates.ViewModels
{
    using System;

    public class AppointmentEmail
    {
        public DateTime AppointmentDate { get; set; }

        public string AppointmentStatus { get; set; }

        public string DeclineReason { get; set; }

        public string DoctorName { get; set; }
    }
}
