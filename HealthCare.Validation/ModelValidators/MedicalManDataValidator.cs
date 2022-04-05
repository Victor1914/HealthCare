namespace HealthCare.Validation.ModelValidators
{
    using FluentValidation;

    using Contracts.Models.MedicalManAccount.Requests;
    using Extensions;

    public class MedicalManDataValidator : BaseValidator<PersistPersonalDataRequest>
    {
        public MedicalManDataValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.GeneralDataData)
                .VerifyName()
                .VerifyIdentityNumber();
        }
    }
}
