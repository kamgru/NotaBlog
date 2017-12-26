using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotaBlog.Admin.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotaBlog.Admin.Services
{
    public class AuthorizationTokenFactory
    {
        private readonly TokenConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AuthorizationTokenFactory(TokenConfiguration tokenConfiguration)
        {
            _configuration = tokenConfiguration;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public AuthorizationToken Create(string username)
        {
            var header = CreateHeader();
            var expires = DateTime.UtcNow.AddMinutes(_configuration.ValidForMinutes);
            var payload = CreatePayload(username, expires);
            var token = new JwtSecurityToken(header, payload);

            return new AuthorizationToken
            {
                AccessToken = _tokenHandler.WriteToken(token),
                RefreshToken = Guid.NewGuid().ToString("N"),
                Expires = expires
            };
        }

        private JwtHeader CreateHeader()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return new JwtHeader(credentials);
        }

        private JwtPayload CreatePayload(string username, DateTime expires)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            return new JwtPayload(
                _configuration.Issuer,
                _configuration.Audience,
                claims,
                DateTime.UtcNow,
                expires
            );
        }
    }
}
