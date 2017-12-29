using Microsoft.AspNetCore.Identity;
using NotaBlog.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaBlog.Admin.Services
{
    public class LoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccessTokenFactory _tokenFactory;

        public LoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IAccessTokenFactory tokenFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenFactory = tokenFactory;
        }

        public async Task<LoginResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new LoginResult(false);
            }

            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return new LoginResult(false);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (!signInResult.Succeeded)
            {
                return new LoginResult(false);
            }

            return new LoginResult(true)
            {
                AccessToken = _tokenFactory.Create(username),
                RefreshToken = Guid.NewGuid().ToString("N")
            };
        }
    }
}
