namespace HealthCare.Contracts.Models.MedicalCenterAccount.Responses
{
    using System.Collections.Generic;
    using Common;
    using Data;

    public class GetMedicalCentersResponse : TokenData
    {
        public List<MedicalCenterData> MedicalCenters { get; set; }
    }
}
