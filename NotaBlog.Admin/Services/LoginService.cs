using Microsoft.AspNetCore.Identity;
using NotaBlog.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaBlog.Admin.Services
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public AuthorizationToken AuthorizationToken { get; set; }

        public AuthenticationResult(bool success) => Success = success;
    }

    public class LoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthorizationTokenFactory _tokenFactory;

        public LoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IAuthorizationTokenFactory tokenFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenFactory = tokenFactory;
        }

        public async Task<AuthenticationResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return new AuthenticationResult(false);
            }

            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return new AuthenticationResult(false);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (!signInResult.Succeeded)
            {
                return new AuthenticationResult(false);
            }

            return new AuthenticationResult(true)
            {
                AuthorizationToken = _tokenFactory.Create(username)
            };
        }
    }
}
