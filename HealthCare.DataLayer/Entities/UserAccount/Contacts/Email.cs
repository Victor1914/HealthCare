namespace HealthCare.DataLayer.Entities.UserAccount.Contacts
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Base;
    using HealthCare.Interfaces;

    public class Email : SystemData, IContactIdentity
    {
        public int UserContactId { get; set; }

        [ForeignKey("UserContactId")]
        public UserContact UserContact { get; set; }      

        public string EmailAddress { get; set; }
    }
}
