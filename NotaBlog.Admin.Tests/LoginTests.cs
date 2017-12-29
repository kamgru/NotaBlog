using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using NotaBlog.Admin.Models;
using NotaBlog.Admin.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace NotaBlog.Admin.Tests
{
    public class LoginTests
    {
        [Theory]
        [InlineData("test", "test")]
        [InlineData("", "test")]
        [InlineData(null, "test")]
        [InlineData("test", "")]
        [InlineData("test", null)]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ItShouldNotReturnNull(string username, string password)
        {
            Service().Login(username, password)
                .Result.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenUsernameEmpty_ItShouldFail(string username)
        {
            Service().Login(username, "password")
                .Result.Success
                .Should().BeFalse();
        }

        [Fact]
        public void WhenUserNotFound_ItShouldFail()
        {
            var userManager = new FakeUserManager { FindByEmailAsyncResult = null };
            Service(userManager).Login("usernotfound", "password")
                .Result.Success
                .Should().BeFalse();
        }

        [Fact]
        public void WhenSignInFails_ItShouldFail()
        {
            var userManager = new FakeUserManager { FindByEmailAsyncResult = new ApplicationUser() };
            var signInManager = new FakeSignInManager { PasswordSignInAsyncResult = SignInResult.Failed };

            var result = Service(userManager, signInManager).Login("test", "password").Result;

            result.Success.Should().BeFalse();
        }

        [Fact]
        public void WhenSignInSuccessful_ItShouldReturnAuthorizationToken()
        {
            var userManager = new FakeUserManager { FindByEmailAsyncResult = new ApplicationUser() };
            var signInManager = new FakeSignInManager { PasswordSignInAsyncResult = SignInResult.Success };

            var token = new AccessToken
            {
                Token = "testaccesstoken",
                Expires = DateTime.Now.AddHours(1),
            };
            var tokenFactory = new Mock<IAccessTokenFactory>();
            tokenFactory.Setup(x => x.Create(It.IsAny<string>()))
                .Returns(token);

            var result = Service(userManager, signInManager, tokenFactory.Object)
                .Login("username", "password").Result;

            result.Success.Should().BeTrue();
            result.AccessToken.Should().BeEquivalentTo(token);
        }

        private LoginService Service(FakeUserManager userManager = null, FakeSignInManager signInManager = null,
            IAccessTokenFactory authorizationTokenFactory = null)
        {
            if (userManager == null)
            {
                userManager = new FakeUserManager();
            }

            if (signInManager == null)
            {
                signInManager = new FakeSignInManager
                {
                    PasswordSignInAsyncResult = new SignInResult()
                };
            }

            if (authorizationTokenFactory == null)
            {
                authorizationTokenFactory = new Mock<IAccessTokenFactory>().Object;
            }

            return new LoginService(userManager, signInManager, authorizationTokenFactory);
        }
    }
}
