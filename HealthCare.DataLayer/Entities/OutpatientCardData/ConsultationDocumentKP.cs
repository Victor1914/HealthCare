namespace HealthCare.DataLayer.Entities.OutpatientCardData
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Base;
    using HealthCare.Interfaces;
    using MedicalData;

    public class ConsultationDocumentKP : SystemData, IMedicalData
    {
        public int CodeSpecialist { get; set; }

        public int CardTelk { get; set; }

        public int? OutpatientCardId { get; set; }
        
        [ForeignKey("OutpatientCardId")]
        public OutpatientCard OutpatientCard { get; set; }

        public int? MedicalProfileId { get; set; }
    }
}
