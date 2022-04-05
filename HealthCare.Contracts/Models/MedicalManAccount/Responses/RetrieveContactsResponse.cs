namespace HealthCare.Contracts.Models.MedicalManAccount.Responses
{
    using System.Collections.Generic;
    using Common;
    using UserAccount.Data;

    public class RetrieveContactsResponse : TokenData
    {
        public List<EmailData> Emails { get; set; }

        public List<PhoneData> PhoneNumbers { get; set; }
    }
}
