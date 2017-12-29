using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NotaBlog.Admin.Data;
using NotaBlog.Admin.Models;
using NotaBlog.Admin.Services;

namespace NotaBlog.Admin.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        private readonly TokenConfiguration _tokenConfiguration;

        private static ConcurrentDictionary<string, string> _refreshTokens = new ConcurrentDictionary<string, string>();

        public AccountController(LoginService loginService, TokenConfiguration tokenConfiguration)
        {
            _loginService = loginService;
            _tokenConfiguration = tokenConfiguration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginService.Login(model.Email, model.Password);
                if (result.Success)
                {
                    _refreshTokens.AddOrUpdate(model.Email, result.RefreshToken, (key, prev) => result.RefreshToken);
                }

                return result.Success
                    ? (IActionResult)Ok(new { AccessToken = result.AccessToken.Token, result.AccessToken.Expires, result.RefreshToken})
                    : BadRequest();
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("renew-access")]
        public async Task<IActionResult> RefreshAccessToken([FromBody]RefreshAccessModel model)
        {
            if (ModelState.IsValid)
            {
                var handler = new JwtSecurityTokenHandler();
                handler.InboundClaimTypeMap.Clear();

                var validationParams = new TokenValidationParameters
                {
                    ValidIssuer = _tokenConfiguration.Issuer,
                    ValidAudience = _tokenConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                };

                try
                {
                    var claimsPrincipal = handler.ValidateToken(model.AccessToken, validationParams, out _);
                    var userId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

                    if (userId == null)
                    {
                        return BadRequest();
                    }

                    if (!_refreshTokens.ContainsKey(userId))
                    {
                        return BadRequest();
                    }

                    if (_refreshTokens[userId] != model.RefreshToken)
                    {
                        return BadRequest();
                    }

                    var accessToken = new AccessTokenFactory(_tokenConfiguration).Create(userId);
                    return Ok(new { AccessToken = accessToken.Token, accessToken.Expires, model.RefreshToken });
                }
                catch (SecurityTokenException)
                {
                    return BadRequest();
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }

            return BadRequest();
        }
    }
}