namespace HealthCare.Contracts.Configuration
{
    public class JwtAuthentication
    {
        public string SecurityKey { get; set; }

        public int ExpirationInMinutes { get; set; }
    }
}
