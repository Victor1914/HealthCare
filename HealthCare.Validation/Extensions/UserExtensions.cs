namespace HealthCare.Validation.Extensions
{
    using System;
    using System.Collections.Generic;
    using FluentValidation;

    using Contracts.Models.UserAccount.Data;
    using Contracts.Models.UserAccount.Requests;
    using Helpers;
    using Utilities.Enums;
    using Utilities.Extensions;

    public static class UserExtensions
    {
        public static IRuleBuilderOptions<T, RegisterUserRequest> VerifyUserName<T>(this IRuleBuilder<T, RegisterUserRequest> ruleBuilder)
        {
            return ruleBuilder.Must(request =>
                    request.GeneralData.Username != null && request.GeneralData.Username.Length.Between(6, 50))
                .WithMessage("Invalid Username")
                .WithErrorCode("InvalidUsername.");
        }

        public static IRuleBuilderOptions<T, RegisterUserRequest> VerifyPassword<T>(this IRuleBuilder<T, RegisterUserRequest> ruleBuilder)
        {
            return ruleBuilder.Must(request =>
                    request.GeneralData.Password != null && request.GeneralData.Password.Length.Between(6, 50))
                .WithMessage("Invalid Password")
                .WithErrorCode("InvalidPassword.");
        }

        public static IRuleBuilderOptions<T, RegisterUserRequest> VerifyUserRole<T>(this IRuleBuilder<T, RegisterUserRequest> ruleBuilder)
        {
            return ruleBuilder.Must(request => Enum.IsDefined(typeof(RoleType), request.UserRole))
                .WithMessage("Invalid user role")
                .WithErrorCode("InvalidUserRole");
        }

        public static IRuleBuilderOptions<T, List<EmailData>> VerifyEmails<T>(this IRuleBuilder<T, List<EmailData>> ruleBuilder)
        {
            return ruleBuilder.Must(emails => emails.Count > 0 && emails.HasValidEmails())
                .WithMessage("Request contains one or more invalid emails.")
                .WithErrorCode("InvalidEmail.");
        }

        public static IRuleBuilderOptions<T, List<PhoneData>> VerifyPhoneNumbers<T>(this IRuleBuilder<T, List<PhoneData>> ruleBuilder)
        {
            return ruleBuilder.Must(phones => phones.Count > 0 && phones.HasValidPhoneNumbers())
                .WithMessage("Request contains one or more invalid phone numbers.")
                .WithErrorCode("InvalidPhoneNumber");
        }

        public static IRuleBuilderOptions<T, List<AddressData>> VerifyAddresses<T>(this IRuleBuilder<T, List<AddressData>> ruleBuilder)
        {
            return ruleBuilder.Must(addresses => addresses.Count > 0)
                .WithMessage("Request contains one or more invalid addresses")
                .WithErrorCode("InvalidAddress");
        }
    }
}
