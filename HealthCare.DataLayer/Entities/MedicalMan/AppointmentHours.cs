namespace HealthCare.DataLayer.Entities.MedicalMan
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Base;
    using MedicalCenter;
    using Utilities.Enums;
    using Utilities.Enums.Appointment;

    public class AppointmentHours : SystemData
    {
        public int MedicalManInfoId { get; set; }

        [ForeignKey("MedicalManInfoId")]
        public MedicalManInfo MedicalManInfo { get; set; }

        public int MedicalCenterInfoId { get; set; }

        [ForeignKey("MedicalCenterInfoId")]
        public MedicalCenterInfo MedicalCenterInfo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public AppointmentHourStatus AppointmentHourStatus { get; set; }
    }
}
