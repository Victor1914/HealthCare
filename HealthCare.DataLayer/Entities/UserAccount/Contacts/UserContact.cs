namespace HealthCare.DataLayer.Entities.UserAccount.Contacts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Base;

    public class UserContact : SystemData
    {
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<Email> Emails { get; set; } = new List<Email>();

        public List<Address> Addresses { get; set; } = new List<Address>();

        public List<Phone> Phones { get; set; } = new List<Phone>();

    }
}
