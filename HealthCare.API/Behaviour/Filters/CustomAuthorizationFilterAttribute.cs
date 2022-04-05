namespace HealthCare.API.Behaviour.Filters
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    using Utilities.Enums;
    using Contracts.Interfaces;
    using Extensions;

    public class CustomAuthorizationFilterAttribute : TypeFilterAttribute
    {
        public CustomAuthorizationFilterAttribute(params RoleType[] permissions) : base(typeof(CustomAuthorizationAttribute))
        {
            Arguments = new object[] { permissions };
        }

        private class CustomAuthorizationAttribute : IAuthorizationFilter
        {
            private ISessionResolver SessionResolver { get; }

            private readonly RoleType[] _permissions;

            public CustomAuthorizationAttribute(ISessionResolver sessionResolver, RoleType[] permissions)
            {
                SessionResolver = sessionResolver;
                _permissions = permissions;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                if (context.IsAttrDisabled<DisableCustomAuthorizationFilterAttribute>())
                    return;

                var result = SessionResolver.SetSessionInfo(context.HttpContext.Request, _permissions);

                if (result.ResponseStatus == ResponseStatus.Success)
                    return;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new ObjectResult(result);
            }
        }
    }

    public class DisableCustomAuthorizationFilterAttribute : ActionFilterAttribute { }
}