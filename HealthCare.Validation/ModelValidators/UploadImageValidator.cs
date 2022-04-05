namespace HealthCare.Validation.ModelValidators
{
    using Contracts.Models.UserAccount.Requests;
    using Extensions;
    using FluentValidation;

    public class UploadImageValidator : BaseValidator<UploadImagesRequest>
    {
        public UploadImageValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.Images)
                .HasValidMainImagesCount();
        }
    }
}
