namespace HealthCare.BusinessLayer.Extensions
{ 
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    
    using MimeKit;
    using MimeKit.Text;
    using Contracts.Models.Email;
    using HealthCare.Interfaces;
   
    public static class EmailSenderExtensions
    {
        public static IEmailService From(this IEmailService emailService, EmailAddress sender)
        {
            emailService.Message.From.Add(new MailboxAddress(sender.Name, sender.Address));
            return emailService;
        }

        public static IEmailService To(this IEmailService emailService, List<EmailAddress> recipients)
        {
            emailService.Message.To.AddRange(recipients.Select(rec => new MailboxAddress(rec.Name, rec.Address)));
            return emailService;
        }
        
        public static IEmailService Subject(this IEmailService emailService, string subject)
        {
            emailService.Message.Subject = subject;
            return emailService;
        }

        public static async Task<IEmailService> UsingTemplate<TModel>(this IEmailService emailService, string viewPath,
            TModel model)
            where TModel : class
        {
            var textBody = await emailService.RazorRenderer.RenderViewToStringAsync($"{viewPath}.cshtml", model);

            emailService.Message.Body = new TextPart(TextFormat.Html) { Text = textBody };

            return emailService;
        }
    }
}
