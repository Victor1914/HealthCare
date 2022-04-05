namespace HealthCare.BusinessLayer.Helpers
{
    using System.Collections.Generic;

    using Contracts.Models.Email;

    using Templates.ViewModels;

    public static class EmailBuilderHelper
    {
        public static (EmailMessage emailMessage, ForgottenPasswordModel model) BuildForgottenPasswordEmail(string userName, string email, string newPassword)
        {
            var emailSender = new EmailAddress { Name = "HealthCareTeam", Address = "health.care.team3@gmail.com" };
            var emailRecipients = new List<EmailAddress>
            {
                new EmailAddress {Name = userName, Address = email}
            };

            var emailMessage = new EmailMessage
            {
                FromAddress = emailSender,
                ToAddresses = emailRecipients,
                Subject = "Password Reset"
            };
            var model = new ForgottenPasswordModel { Name = userName, Password = newPassword };

            return (emailMessage, model);
        }

        public static (EmailMessage emailMessage, WelcomeEmailModel model) BuildWelcomeEmail(string userName, string email)
        {
            var emailSender = new EmailAddress { Name = "HealthCareTeam", Address = "health.care.team3@gmail.com" };
            var emailRecipients = new List<EmailAddress>
            {
                new EmailAddress {Name = userName, Address = email}
            };

            var emailMessage = new EmailMessage
            {
                FromAddress = emailSender,
                ToAddresses = emailRecipients,
                Subject = "Password Reset"
            };
            var model = new WelcomeEmailModel { Username = userName };

            return (emailMessage, model);
        }
    }
}
