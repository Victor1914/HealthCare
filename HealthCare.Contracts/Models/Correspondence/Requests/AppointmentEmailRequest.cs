namespace HealthCare.Contracts.Models.Correspondence.Requests
{
    using Data;

    public class AppointmentEmail : BaseEmailMessage
    {
        public AppointmentEmail Content { get; set; }
    }
}
