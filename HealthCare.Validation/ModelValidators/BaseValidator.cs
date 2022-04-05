namespace HealthCare.Validation.ModelValidators
{
    using System.Linq;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Mvc;

    public class BaseValidator<T> : AbstractValidator<T>, IValidatorInterceptor
    {
        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext, ValidationContext validationContext)
        {
            return validationContext;
        }

        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext, ValidationResult result)
        {
            var projection = result.Errors.Select(failure => new ValidationFailure(failure.ErrorCode, failure.ErrorMessage));
            return new ValidationResult(projection);
        }
    }
}
