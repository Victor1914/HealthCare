namespace HealthCare.Contracts.Models.PatientAccount.Responses
{
    using System.Collections.Generic;
    using Common;
    using Data;
    using OutpatientCard.Data;

    public class GetOutPatientCardsByEgnResponse : TokenData
    {
        public List<OutPatientCardInfo> PatientCars { get; set; }
    }
}
