namespace HealthCare.Validation.ModelValidators
{
    using FluentValidation;

    using Contracts.Models.UserAccount.Requests;
    using Extensions;

    public class EditUserGeneraDataValidator : BaseValidator<EditUserGeneraDataRequest>
    {
        public EditUserGeneraDataValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request.Contacts.Emails)
                .VerifyEmails();

            RuleFor(request => request.Contacts.Phones)
                .VerifyPhoneNumbers();
        }
    }
}
