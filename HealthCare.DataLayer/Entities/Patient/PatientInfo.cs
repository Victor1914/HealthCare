namespace HealthCare.DataLayer.Entities.Patient
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Base;
    using Interfaces;
    using UserAccount;
    using Utilities.Enums;

    public class PatientInfo : SystemData, IInformation
    {
        public string Name { get; set; }

        public long EGN { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User Patient { get; set; }
    }
}
