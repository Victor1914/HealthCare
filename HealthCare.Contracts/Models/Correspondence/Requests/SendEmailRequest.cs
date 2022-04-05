namespace HealthCare.Contracts.Models.Correspondence.Requests
{
    using Data;
    using Templates.ViewModels;

    public class SendEmailRequest : BaseEmailMessage
    {
        public SendEmail Content { get; set; }
    }
}
