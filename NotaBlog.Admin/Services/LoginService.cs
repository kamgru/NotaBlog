using Microsoft.AspNetCore.Identity;
using NotaBlog.Admin.Data;
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
        private readonly ApplicationDbContext _applicationDbContext;

        public LoginService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IAccessTokenFactory tokenFactory, 
            ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenFactory = tokenFactory;
            _applicationDbContext = applicationDbContext;
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

            var refreshToken = _applicationDbContext.RefreshTokens.Find(username);
            if (refreshToken == null)
            {
                refreshToken = new RefreshToken
                {
                    UserId = username,
                    Token = Guid.NewGuid().ToString("N")
                };
                _applicationDbContext.RefreshTokens.Add(refreshToken);
                _applicationDbContext.SaveChanges();
            }

            return new LoginResult(true)
            {
                AccessToken = _tokenFactory.Create(username),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
