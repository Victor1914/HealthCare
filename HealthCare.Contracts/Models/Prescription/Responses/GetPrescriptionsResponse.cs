namespace HealthCare.Contracts.Models.Prescription.Responses
{
    using System.Collections.Generic;
    using Common;
    using Data;

    public class GetPrescriptionsResponse : TokenData
    {
        public List<ExtendedPrescriptionData> Prescriptions { get; set; }
    }

    public class ExtendedPrescriptionData : PrescriptionData
    {
        public string DoctorName { get; set; }

        public string DoctorSpecialty { get; set; }

        public string CreationDate { get;set; }
    }
}
