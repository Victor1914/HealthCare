namespace HealthCare.Interfaces
{
    using System.Threading.Tasks;
    using MimeKit;

    public interface IEmailService
    {
        IRazorViewToStringRenderer RazorRenderer { get; set; }

        MimeMessage Message { get; set; }

        Task Send();
    }
}
