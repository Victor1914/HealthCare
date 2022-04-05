namespace HealthCare.Templates.ViewModels
{
    using System.Collections.Generic;

    public class ShareUser
    {
        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public string SharedUserName { get; set; }

        public List<string> Emails { get; set; }
        
        public List<string> Phones { get; set; }

        public List<string> Addresses { get; set; }
    }
}
