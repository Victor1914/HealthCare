namespace HealthCare.Contracts.Models.Correspondence.Requests
{
    using Data;
    using Templates.ViewModels;

    public class ShareUserRequest : BaseEmailMessage
    {
        public ShareUser Content { get; set; }
    }
}
