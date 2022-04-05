namespace HealthCare.Contracts.Models.PatientAccount.Data
{
    using Utilities.Enums;

    public class MedicalProfileGeneralData
    {
        public int Height { get; set; }

        public int Weight { get; set; }

        public Level PhysicalActivityLevel { get; set; }

        public Level StressLevel { get; set; }

        public BloodGroup BloodGroup { get; set; }
    }
}
