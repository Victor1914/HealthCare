namespace HealthCare.Validation.ModelValidators
{
    using FluentValidation;

    using Contracts.Models.UserAccount.Requests;
    using Extensions;
    using Utilities.Enums;

    public class RegisterUserValidator : BaseValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(request => request)
                .VerifyName()
                .VerifyUserRole();

            When(request => request.UserRole == RoleType.Doctor, () =>
                {
                    RuleFor(request => request)
                        .VerifyUserName()
                        .VerifyPassword();
                });
        }
    }
}

