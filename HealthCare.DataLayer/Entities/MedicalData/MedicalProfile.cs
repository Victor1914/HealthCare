namespace HealthCare.DataLayer.Entities.MedicalData
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Base;
    using Patient;
    using Utilities.Enums;

    public class MedicalProfile : SystemData
    {
        public int Height { get; set; }

        public int Weight { get; set; }

        public Level PhysicalActivityLevel { get; set; }

        public Level StressLevel { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public int PatientInfoId { get; set; }

        [ForeignKey("PatientInfoId")]
        public PatientInfo PatientInfo { get; set; }

        public List<Illness> MedicalProfileIllnesses { get; set; }  = new List<Illness>();

        public List<MedicalTest> MedicalProfileMedicalTests { get; set; } = new List<MedicalTest>();
    }
}
