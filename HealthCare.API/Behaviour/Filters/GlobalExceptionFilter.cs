namespace HealthCare.API.Behaviour.Filters
{
    using System.Net;
    using Contracts.Models.Common;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(new ErrorResponse { Exception = context.Exception });

            context.ExceptionHandled = true;
        }
    }
}
