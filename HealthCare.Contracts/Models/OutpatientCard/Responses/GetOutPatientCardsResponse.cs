namespace HealthCare.Contracts.Models.OutpatientCard.Responses
{
    using System.Collections.Generic;
    using Common;
    using Data;

    public class GetOutPatientCardsResponse : TokenData
    {
        public List<OutPatientCardInfo> OutPatientCardInfo { get; set; }
    }
}
