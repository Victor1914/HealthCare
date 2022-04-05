namespace HealthCare.DataLayer.Entities.MedicalCenter
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Base;
    using Interfaces;
    using MedicalMan;
    using UserAccount;
    using Utilities.Enums;

    public class MedicalCenterInfo : SystemData, IInformation
    {
        public string Name { get; set; }

        public string Details { get; set; }

        public bool IsNzok { get; set; }

        public bool HasEmergency { get; set; }

        public MedicalCenterType Type { get; set; }

        public int UserId { get; set; }

        public Gender Gender { get; set; } = Gender.None;

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<AppointmentHours> AppointmentHours { get; set; } = new List<AppointmentHours>();
    }
}
