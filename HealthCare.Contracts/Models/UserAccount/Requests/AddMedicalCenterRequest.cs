namespace HealthCare.Contracts.Models.UserAccount.Requests
{
    using Data;

    public class AddMedicalCenterRequest
    {
        public string Name { get; set; }

        public AddressData Address { get; set; }
    }
}
