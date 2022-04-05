namespace HealthCare.BusinessLayer.Services
{
    using System.Threading.Tasks;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;

    using Contracts.Configuration;
    using Contracts.Models.Correspondence.Responses;
    using Contracts.Models.Email;
    using Extensions;
    using HealthCare.Interfaces;
    using Interfaces;
    using Microsoft.Extensions.Options;

    public class CommunicationService : ICommunicationService
    {
        private readonly IOptionsSnapshot<TwilioConfiguration> _twilioConfiguration;

        private readonly IEmailService _emailService;

        public CommunicationService(IEmailService emailService, IOptionsSnapshot<TwilioConfiguration> twilioConfiguration)
        {
            _emailService = emailService;
            _twilioConfiguration = twilioConfiguration;
        }

        public async Task<SendEmailResponse> SendEmail<TViewModel>(EmailMessage message, TViewModel model)
            where TViewModel : class
        {
            await _emailService
                 .From(message.FromAddress)
                 .To(message.ToAddresses)
                 .Subject(message.Subject)
                 .UsingTemplate($"/Views/Emails/{typeof(TViewModel).Name}", model);

            await _emailService.Send();

            return new SendEmailResponse
            {
                Result = true
            };
        }

        public async Task<bool> SendSms(string receiverPhoneNumber)
        {
            TwilioClient.Init(_twilioConfiguration.Value.AccountSId, _twilioConfiguration.Value.AuthToken);

            var result = await MessageResource.CreateAsync(
                body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                from: new Twilio.Types.PhoneNumber(_twilioConfiguration.Value.SenderNumber),
                to: new Twilio.Types.PhoneNumber(receiverPhoneNumber)
            );

            return result.Status == MessageResource.StatusEnum.Sent;
        }
    }
}
