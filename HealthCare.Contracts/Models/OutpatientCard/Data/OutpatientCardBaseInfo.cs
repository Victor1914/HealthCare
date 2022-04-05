namespace HealthCare.Contracts.Models.OutpatientCard.Data
{
    using System;
    using System.Collections.Generic;

    using CommonMedicalData;
    using Utilities.Enums.Visitation;

    public class OutpatientCardBaseInfo
    {
        public int PatientId { get; set; }

        public int MedicalCenterId { get; set; }

        public string SpecialtyCode { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string FinancedBy { get; set; }

        public VisitationReason VisitationReason { get; set; }

        public VisitationType VisitationType { get; set; }

        public List<MedicalDocuments> MedicalDocuments { get; set; }

        public List<MedicalDocumentsVSD> MedicalDocumentsVSD { get; set; } 

        public List<MedicalDocumentsMMD> MedicalDocumentsMMD { get; set; }

        public int MECardNumber { get; set; }

        public MedicalDocumentsKP MedicalDocumentsKP { get; set; }

        public SickLeaveData SickLeave { get; set; }

        public int PrescriptionBookId { get; set; }

        public bool QuickTest { get; set; }

        public bool StageEpicrisis { get; set; }

        public bool MedicalNote { get; set; }

        public bool Prescription { get; set; }

        public string MedicalHistory { get; set; }

        public string ObjectiveCondition { get; set; }

        public string Therapies { get; set; }

        public MainIllnessData MainDiagnose { get; set; }

        public List<OutPatientCardIllnessData> OtherDiseases { get; set; }

        public List<AttachmentData> MedicalAttachments { get; set; }
    }
}
