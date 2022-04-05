namespace HealthCare.Contracts.Models.Appointment.Requests
{
    using System;
    using Utilities.Enums;

    public class HourData
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public HourStatus HourStatus { get; set; } = HourStatus.NotModified;
    }
}
