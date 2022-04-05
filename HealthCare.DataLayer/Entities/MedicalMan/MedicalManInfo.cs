namespace HealthCare.DataLayer.Entities.MedicalMan
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Base;
    using Interfaces;
    using UserAccount;
    using Utilities.Enums;

    public class MedicalManInfo : SystemData, IInformation
    {
        public long IdentityNumber { get; set; }

        public string Name { get; set; }

        public string Biography { get; set; }

        public Gender Gender { get; set; }

        public bool IsNzok { get; set; }

        public bool IsAdditionalHealthInsurance { get; set; }

        public int ExperienceInYears { get; set; }

        public DateTime BirthDate {get;set;}

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        
        public List<MedicalManSpecialty> Specialties { get; set; } = new List<MedicalManSpecialty>();

        public List<AppointmentHours> AppointmentHours { get; set; } = new List<AppointmentHours>();
    }
}
