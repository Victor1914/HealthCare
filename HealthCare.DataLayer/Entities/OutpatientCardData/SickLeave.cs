namespace HealthCare.DataLayer.Entities.OutpatientCardData
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Base;
    using HealthCare.Interfaces;
    using MedicalData;

    public class SickLeave : SystemData, IMedicalData
    {
        public long DocumentNumber { get; set; }

        public int DaysCount { get; set; }

        public string MKBCode { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int? OutpatientCardId { get; set; }
        
        [ForeignKey("OutpatientCardId")]
        public OutpatientCard OutpatientCard { get; set; }
        
        public int? MedicalProfileId { get; set; }
        
    }
}
