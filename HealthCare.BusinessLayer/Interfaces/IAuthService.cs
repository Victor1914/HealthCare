namespace HealthCare.BusinessLayer.Interfaces
{
    public interface IAuthService
    {
        (string, int) RetrieveTokenData(string token);

        string GenerateToken(string username, int userId);
    }
}