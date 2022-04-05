namespace HealthCare.Contracts.Models.UserAccount.Data
{
    using Utilities.Enums;

    public class PhoneData
    {
        public int Id { get; set; }

        public PhoneType Type { get; set; }

        public string Number { get; set; }

        public bool IsDeleted { get; set; }
    }
}
