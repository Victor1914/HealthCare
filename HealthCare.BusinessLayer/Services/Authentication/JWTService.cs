namespace HealthCare.BusinessLayer.Services.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
   
    using Contracts.Configuration;
    using Exceptions.JWTExceptions;
    using Interfaces;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Utilities.Helpers;

    public class JWTService : IAuthService
    {
        private readonly IOptionsSnapshot<JwtAuthentication> _config;

        private const string ClaimUserId = "userId";
        private const string ClaimUserName = "userName";

        public JWTService(IOptionsSnapshot<JwtAuthentication> config)
        {
            _config = config;
        }

        public (string, int) RetrieveTokenData(string token)
        {
            token = token.Replace("Bearer ", "");
            ValidationUtils.ValidateAndThrow<InvalidTokenException>(() => string.IsNullOrEmpty(token));

            var tokenClaims = ExtractClaims(token);

            var userId = tokenClaims.Find(x => x.Type == ClaimUserId).Value;
            var userName = tokenClaims.Find(x => x.Type == ClaimUserName).Value;
            int.TryParse(userId, out var id);

            return (userName, id);
        }

        public string GenerateToken(string username, int userId)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimUserName, username),
                    new Claim(ClaimUserId, userId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config.Value.ExpirationInMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                        Convert.FromBase64String(_config.Value.SecurityKey)), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_config.Value.SecurityKey))
            };
        }

        private List<Claim> ExtractClaims(string token)
        {
            try
            {
                var validationParameters = GetTokenValidationParameters();

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenValid = tokenHandler.ValidateToken(token, validationParameters, out _);

                return tokenValid.Claims.ToList();
            }
            catch (SecurityTokenExpiredException)
            {
                throw new ExpiredTokenException();
            }
            catch (Exception)
            {
                throw new InvalidTokenException();
            }
        }
    }
}
