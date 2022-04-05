namespace HealthCare.Validation.ModelValidators
{
    using FluentValidation;

    using Contracts.Models.Appointment.Requests;
    
    public class AppointmentStatusValidator : BaseValidator<ChangeAppointmentStatusRequest>
    {
        public AppointmentStatusValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.Status)
                .IsInEnum();
        }
    }
}
