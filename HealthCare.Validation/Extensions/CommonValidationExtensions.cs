namespace HealthCare.Validation.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Models.Appointment.Requests;
    using Contracts.Models.MedicalManAccount.Data;
    using Contracts.Models.UserAccount.Data;
    using FluentValidation;
    using Helpers;
    using Interfaces;
    using Utilities.Extensions;

    public static class CommonValidationExtensions
    {
        public static IRuleBuilderOptions<T, TModel> VerifyName<T, TModel>(this IRuleBuilder<T, TModel> ruleBuilder)
            where TModel : IName
        {
            return ruleBuilder.Must(request => request.Name.Length.Between(4, 70))
                .WithMessage("Invalid name.")
                .WithErrorCode("InvalidName");
        }

        public static IRuleBuilderOptions<T, MedicalManGeneralData> VerifyIdentityNumber<T>(this IRuleBuilder<T, MedicalManGeneralData> ruleBuilder)
        {
            return ruleBuilder.Must(request => /*request.IdentityNumber.ToString().Length >= 10 
                                               &&*/ request.IdentityNumber.ToString().All(char.IsDigit)
                                               && request.IdentityNumber.ToString().Any(ch => ch != 0))
                .WithMessage("Invalid uin number")
                .WithErrorCode("InvalidUIN");
        }

        public static IRuleBuilderOptions<T, List<ImageData>> HasValidMainImagesCount<T>(this IRuleBuilder<T, List<ImageData>> ruleBuilder)
        {
            return ruleBuilder.Must(images => images.Count(img => img.IsMain) <= 1)
                .WithMessage("Invalid main images count in request.")
                .WithErrorCode("InvalidMainImgCount");
        }

        public static IRuleBuilderOptions<T, List<HourData>> HasOverlappingHours<T>(this IRuleBuilder<T, List<HourData>> ruleBuilder)
        {
            return ruleBuilder.Must(hours => hours.Count == hours.Select(hr => hr.StartDate).Distinct().Count())
                .WithMessage("Your request contains overlapping hours.")
                .WithErrorCode("InvalidAppointmentHours");
        }

        public static IRuleBuilderOptions<T, List<HourData>> HasNotRepeatedHours<T>(this IRuleBuilder<T, List<HourData>> ruleBuilder)
        {
            return ruleBuilder.Must(hours => hours.Count == hours.Select(hr => new{ hr.StartDate, hr.EndDate}).Distinct().Count())
                .WithMessage("Your request contains hours with same values.")
                .WithErrorCode("InvalidAppointmentHours");

        }
    }
}
