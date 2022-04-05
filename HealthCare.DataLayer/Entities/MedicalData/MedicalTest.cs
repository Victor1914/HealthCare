namespace HealthCare.DataLayer.Entities.MedicalData
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Base;
    using HealthCare.Interfaces;

    public class MedicalTest : SystemData, IMedicalData
    {
        public int? MedicalProfileId { get; set; }

        public int? OutpatientCardId { get; set; }

        [ForeignKey("OutpatientCardId")]
        public OutpatientCard OutpatientCard { get; set; }

        public List<MedicalTestAttachment> MedicalTestAttachments { get; set; }  = new List<MedicalTestAttachment>();
    }
}
