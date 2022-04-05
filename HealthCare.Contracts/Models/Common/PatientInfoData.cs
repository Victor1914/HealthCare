namespace HealthCare.Contracts.Models.Common
{
    using System;
    using Utilities.Enums;

    public class PatientInfoData
    {
        public int PatientInfoId {get;set;}

        public string Name {get;set;}

        public string Address { get;set; }

        public string Email {get;set;}

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public bool IsNewPatient { get;set; }

        public DateTime BirthDate {get; set; }
    }
}
