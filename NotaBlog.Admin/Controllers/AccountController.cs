using System;
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
using NotaBlog.Admin.Models;
using NotaBlog.Admin.Services;

namespace NotaBlog.Admin.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;

        public AccountController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginService.Login(model.Email, model.Password);
                return result.Success
                    ? (IActionResult)Ok(result.AuthorizationToken)
                    : BadRequest();
            }

            return BadRequest();
        }
    }
}