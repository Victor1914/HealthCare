namespace HealthCare.BusinessLayer.Services
{
    using System.Threading.Tasks;
    using Contracts.Configuration;
    using HealthCare.Interfaces;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.Extensions.Options;
    using MimeKit;

    public class EmailService : IEmailService
    {
        private readonly IOptionsSnapshot<EmailConfiguration> _emailConfig;

        public IRazorViewToStringRenderer RazorRenderer { get; set; }

        public MimeMessage Message { get; set; } = new MimeMessage();

        public EmailService(IOptionsSnapshot<EmailConfiguration> emailConfig, IRazorViewToStringRenderer razorRenderer)
        {
            _emailConfig = emailConfig;
            RazorRenderer = razorRenderer;
        }

        public async Task Send()
        {
            var emailClient = new SmtpClient();
            emailClient.Connect(_emailConfig.Value.SmtpHost, _emailConfig.Value.SmtpPort, SecureSocketOptions.StartTls);
            
            emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
            emailClient.Authenticate(_emailConfig.Value.SmtpUsername, _emailConfig.Value.SmtpPassword);

            using var client = emailClient;
            await emailClient.SendAsync(Message);
        }
    }
}
