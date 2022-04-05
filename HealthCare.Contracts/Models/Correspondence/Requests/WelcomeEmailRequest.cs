namespace HealthCare.Contracts.Models.Correspondence.Requests
{
    using Data;
    using Templates.ViewModels;

    public class WelcomeEmailRequest : BaseEmailMessage
    {
        public WelcomeEmailModel Content { get; set; }
    }
}
