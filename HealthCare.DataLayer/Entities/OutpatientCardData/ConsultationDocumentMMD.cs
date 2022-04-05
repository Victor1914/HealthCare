namespace HealthCare.DataLayer.Entities.OutpatientCardData
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Base;
    using HealthCare.Interfaces;
    using MedicalData;

    public class ConsultationDocumentMMD : SystemData, IMedicalData
    {
        public int DocumentNumber { get; set; }

        public int CodeTests1 { get; set; }

        public int CodeTests2 { get; set; }

        public int CodeTests3 { get; set; }

        public int CodeTests4 { get; set; }

        public int? MedicalProfileId { get; set; }
        
        public int? OutpatientCardId { get; set; }
        
        [ForeignKey("OutpatientCardId")]
        public OutpatientCard OutpatientCard { get; set; }
    }
}