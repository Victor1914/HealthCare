namespace HealthCare.Validation.ModelValidators
{
    using Contracts.Models.Appointment.Requests;
    using Extensions;
    using FluentValidation;

    public class AddAppointmentValidator : BaseValidator<AddAppointmentHoursRequest>
    {
        public AddAppointmentValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Hours)
                .HasNotRepeatedHours()
                .HasOverlappingHours();
        }
    }
}
