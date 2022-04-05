namespace HealthCare.DataLayer.Entities.MedicalData
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Base;
    using HealthCare.Interfaces;
    using Utilities.Enums;

    public class Illness : SystemData, IMedicalData
    {
        public int IllnessTypeId { get; set; }

        [ForeignKey("IllnessTypeId")]
        public IllnessType IllnessType { get; set; }

        public IllnessPeriod IllnessPeriod { get; set; }

        public DocumentStatus Status { get; set; }

        public string Notes { get; set; }

        public bool IsMain { get;set; }

        public int? MedicalProfileId { get; set; }

        [ForeignKey("MedicalProfileId")]
        public MedicalProfile MedicalProfile { get; set; }

        public int? OutpatientCardId { get; set; }

        [ForeignKey("OutpatientCardId")]
        public OutpatientCard OutpatientCard { get; set; }
    }
}
