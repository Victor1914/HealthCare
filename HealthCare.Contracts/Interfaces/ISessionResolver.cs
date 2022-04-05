namespace HealthCare.Contracts.Interfaces
{
    using Microsoft.AspNetCore.Http;

    using Models.Common;
    using Utilities.Enums;

    public interface ISessionResolver
    {
        SessionInfo SessionInfo { get; }

        ResponseResult SetSessionInfo(HttpRequest request, RoleType [] permissions);
    }
}
