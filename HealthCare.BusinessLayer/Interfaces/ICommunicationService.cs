namespace HealthCare.BusinessLayer.Interfaces
{
    using System.Threading.Tasks;
    using Contracts.Models.Correspondence.Responses;
    using Contracts.Models.Email;

    public interface ICommunicationService
    {
        Task<SendEmailResponse> SendEmail<TViewModel>(EmailMessage message, TViewModel model)
            where TViewModel : class;

        Task<bool> SendSms(string receiverPhoneNumber);
    }
}
