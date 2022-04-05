namespace HealthCare.Contracts.Models.PatientAccount.Requests
{
    using System.Collections.Generic;
    using CommonMedicalData;
    using Data;

    public class PersistMedicalProfileRequest
    {
        public PatientGeneralData PatientData { get; set; }

        public MedicalProfileGeneralData MedicalData { get; set; }

        public List<IllnessData> Illnesses { get; set; }

        public List<AttachmentData> MedicalTests { get; set; }
    }
}
