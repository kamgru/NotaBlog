using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotaBlog.Admin.Models;
using NotaBlog.Admin.Services;

namespace NotaBlog.Admin.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        private readonly RenewAccessService _renewAccessService;

        public AccountController(LoginService loginService, RenewAccessService renewAccessService)
        {
            _loginService = loginService;
            _renewAccessService = renewAccessService;
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
                    return Ok(new AccessInfo
                    {
                        AccessToken = result.AccessToken.Token,
                        Expires = result.AccessToken.Expires,
                        RefreshToken = result.RefreshToken
                    });
                }
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("renew-access")]
        public IActionResult RefreshAccessToken([FromBody]RefreshAccessModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _renewAccessService.RenewAccess(model.AccessToken, model.RefreshToken);
                if (result.Success)
                {
                    return Ok(new AccessInfo
                    {
                        AccessToken = result.AccessToken.Token,
                        Expires = result.AccessToken.Expires,
                        RefreshToken = result.RefreshToken
                    });
                }
            }

            return BadRequest();
        }
    }
}