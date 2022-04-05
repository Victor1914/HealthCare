namespace HealthCare.Contracts.Models.UserAccount.Requests
{
    using Data;

    public class EditUserGeneraDataRequest
    {
        public GeneralUserData GeneralData { get; set; }

        public ContactUserData Contacts { get; set; }
    }
}
