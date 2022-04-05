namespace HealthCare.DataLayer.Entities.UserAccount.Contacts
{
    using System.ComponentModel.DataAnnotations.Schema;
    
    using Base;
    using HealthCare.Interfaces;
    using Utilities.Enums;

    public class Phone : SystemData, IContactIdentity
    {
        public PhoneType Type { get; set; }

        public string Number { get; set; }

        public int UserContactId { get; set; }

        [ForeignKey("UserContactId")]
        public UserContact UserContact { get; set; }

    }
}
