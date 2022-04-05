namespace HealthCare.Contracts.Models.MedicalManAccount.Requests
{
    using System.Collections.Generic;
    using CommonMedicalData;
    using Utilities.Enums;

    public class PersistOutpatientCardRequest
    {
        public int OutpatientCardId { get; set; }

        public int PatientId { get; set; }

        public DocumentStatus Status { get; set; }

        public List<IllnessData> Illnesses { get; set; }

        public List<AttachmentData> MedicalTestAttachments { get; set; }
    }
}
