namespace HealthCare.Contracts.Models.PatientAccount.Data
{
    using System;

    using Utilities.Enums;

    public class PatientGeneralData
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }
    }
}
