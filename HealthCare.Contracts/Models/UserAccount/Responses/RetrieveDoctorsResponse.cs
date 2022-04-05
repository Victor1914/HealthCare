namespace HealthCare.Contracts.Models.UserAccount.Responses
{
    using Data;

    public class RetrieveDoctorsResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public ContactUserData Contacts { get; set; }
    }
}
