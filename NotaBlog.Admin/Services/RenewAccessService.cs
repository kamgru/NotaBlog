using Microsoft.IdentityModel.Tokens;
using NotaBlog.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Admin.Services
{
    public class RenewAccessService
    {
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IAccessTokenFactory _tokenFactory;
        private readonly ISecurityTokenValidator _securityTokenValidator;
        private readonly ApplicationDbContext _applicationDbContext;

        public RenewAccessService(
            TokenConfiguration tokenConfiguration, 
            IAccessTokenFactory tokenFactory, 
            ISecurityTokenValidator securityTokenValidator, 
            ApplicationDbContext applicationDbContext)
        {
            _tokenConfiguration = tokenConfiguration;
            _tokenFactory = tokenFactory;
            _securityTokenValidator = securityTokenValidator;
            _applicationDbContext = applicationDbContext;
        }

        public RenewAccessResult RenewAccess(string accessToken, string refreshToken)
        {
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                return new RenewAccessResult(false);
            }

            try
            {
                var validationParams = new TokenValidationParameters
                {
                    ValidIssuer = _tokenConfiguration.Issuer,
                    ValidAudience = _tokenConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                };

                var claimsPrincipal = _securityTokenValidator.ValidateToken(accessToken, validationParams, out _);
                var userId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return new RenewAccessResult(false);
                }

                var user = _applicationDbContext.Users.FirstOrDefault(x => x.UserName == userId);
                if (user == null)
                {
                    return new RenewAccessResult(false);
                }

                var token = _applicationDbContext.RefreshTokens.Find(userId);
                if (token == null)
                {
                    return new RenewAccessResult(false);
                }

                if (token.Token != refreshToken)
                {
                    return new RenewAccessResult(false);
                }

                return new RenewAccessResult(true)
                {
                    AccessToken = _tokenFactory.Create(userId),
                    RefreshToken = refreshToken
                };
            }
            catch (SecurityTokenException)
            {
                return new RenewAccessResult(false);
            }
        }
    }
}
