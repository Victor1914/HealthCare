namespace HealthCare.Contracts.Models.UserAccount.Data
{
    public class AddressData
    {
        public int Id { get; set; }

        public string StreetAddress { get; set; }

        public int CityId { get; set; }

        public bool IsCurrent { get; set; }
    }
}
