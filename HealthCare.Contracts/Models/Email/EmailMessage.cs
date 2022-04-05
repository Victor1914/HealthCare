namespace HealthCare.Contracts.Models.Email
{
    using System.Collections.Generic;

    public class EmailMessage
    {
        public string Subject { get; set; }

        public EmailAddress FromAddress { get; set; } = new EmailAddress();

        public List<EmailAddress> ToAddresses { get; set; } = new List<EmailAddress>();
    }
}
