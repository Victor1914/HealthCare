namespace HealthCare.DataLayer.Entities.MedicalData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using Base;
    using OutpatientCardData;
    using UserAccount;
    using Utilities.Enums;
    using Utilities.Enums.Visitation;

    public class OutpatientCard : SystemData
    {
        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public VisitationReason VisitationReason { get; set; }

        public VisitationType VisitationType { get; set; }

        public DocumentStatus Status { get; set; }

        public string FinancedBy { get; set; }

        public int PrescriptionBookId { get; set; }

        public bool QuickTest { get; set; }

        public bool StageEpicrisis { get; set; }

        public bool MedicalNote { get; set; }

        public bool Prescription { get; set; }

        public int MainDiagnoseId { get; set; }

        public string MedicalHistory { get; set; }

        public string ObjectiveCondition { get;set; }

        public string Therapies { get;set; }

        public string SpecialtyCode { get; set; }

        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public User Doctor { get; set; }

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public User Patient { get; set; }

        public int MedCenterId { get; set; }

        [ForeignKey("MedCenterId")]
        public User MedCenter { get; set; }

        public List<ConsultationDocument> ConsultationDocuments { get; set; } = new List<ConsultationDocument>();

        public List<ConsultationDocumentMMD> ConsultationDocumentsMMD { get; set; } = new List<ConsultationDocumentMMD>();
        
        public List<MedicalTest> OutpatientCardMedicalTests { get; set; }  = new List<MedicalTest>();

        public List<Illness> OutpatientCardIllnesses { get; set; }  = new List<Illness>();
    }
}
