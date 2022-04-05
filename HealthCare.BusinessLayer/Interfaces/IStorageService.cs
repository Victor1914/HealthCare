namespace HealthCare.BusinessLayer.Interfaces
{
    using Utilities.Enums;

    public interface IStorageService
    {
        RoleType RetrieveUserRole(int userId, string username);
    }
}
