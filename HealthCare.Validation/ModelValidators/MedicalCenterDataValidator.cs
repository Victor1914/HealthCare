namespace HealthCare.Validation.ModelValidators
{
    using FluentValidation;

    using Contracts.Models.MedicalCenterAccount.Requests;
    using Extensions;
    
    public class MedicalCenterDataValidator : BaseValidator<PersistMedicalCenterDataRequest>
    {
        public MedicalCenterDataValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.Data)
                .VerifyName();
        }
    }
}
