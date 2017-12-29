using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NotaBlog.Admin.Data;
using NotaBlog.Admin.Models;
using NotaBlog.Admin.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace NotaBlog.Admin.Tests
{
    public class RenewAccessTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData(null, "")]
        [InlineData("accessToken", "")]
        [InlineData("accessToken", null)]
        [InlineData("", "refreshToken")]
        [InlineData(null, "refreshToken")]
        public void WhenAnyTokenNullOrEmpty_ItShouldFail(string accessToken, string refreshToken)
        {
            Service().RenewAccess(accessToken, refreshToken)
                .Success.Should().BeFalse();
        }

        [Fact]
        public void WhenTokenValidationFails_ItShouldFail()
        {
            Service(validator: FailingValidator())
                .RenewAccess("accessToken", "refreshToken")
                .Success.Should().BeFalse();
        }

        [Fact]
        public void WhenNoClaimOfType_sub_InToken_ItShouldFail()
        {
            var claims = new FakeClaimsPrincipal(new Claim[0]);
            var validator = SuccessfulValidator(claims);

            Service(validator)
                .RenewAccess("accessToken", "refreshToken")
                .Success.Should().BeFalse();
        }

        [Fact]
        public void WhenNoUserIdInToken_ItShouldFail()
        {
            var claims = new FakeClaimsPrincipal(new Claim("sub", string.Empty));
            var validator = SuccessfulValidator(claims);

            Service(validator)
                .RenewAccess("accessToken", "refreshToken")
                .Success.Should().BeFalse();
        }

        [Fact]
        public void WhenUserNotFound_ItShouldFail()
        {
            var validator = SuccessfulValidatorForUser("nosuchuser@notablog.com");

            Service(validator, applicationDbContext: DbContext())
                .RenewAccess("accessToken", "refreshToken")
                .Success.Should().BeFalse();
        }

        [Fact]
        public void WhenRefreshTokenForUserNotFound_ItShouldFail()
        {
            var username = "user@notablog.com";
            var validator = SuccessfulValidatorForUser(username);
            var dbContext = DbContext(username, refreshToken: null);

            Service(validator, applicationDbContext: dbContext)
                .RenewAccess("accessToken", "refreshToken")
                .Success.Should().BeFalse();
        }

        [Fact]
        public void WhenRefreshTokenForUserDoesNotMatch_ItShouldFail()
        {
            var username = "user@notablog.com";
            var validator = SuccessfulValidatorForUser(username);
            var dbContext = DbContext(username, "refreshToken");

            Service(validator, applicationDbContext: dbContext)
                .RenewAccess("accessToken", "invalidRefreshToken")
                .Success.Should().BeFalse();
        }

        [Fact]
        public void GivenValidTokens_ItShouldReturnNewAccessToken()
        {
            var username = "user@notablog.com";
            var validator = SuccessfulValidatorForUser(username);
            var dbContext = DbContext(username, "refreshToken");
            var factory = TokenFactory("test-access-token");

            var result = Service(validator, factory, dbContext)
                .RenewAccess("accessToken", "refreshToken");

            result.Success.Should().BeTrue();
            result.AccessToken.Token.Should().BeEquivalentTo("test-access-token");
            result.AccessToken.Expires.Should().BeOnOrAfter(DateTime.Now);
        }

        [Fact]
        public void GivenValidTokens_ItShouldReturnSameRefreshToken()
        {
            var username = "user@notablog.com";
            var validator = SuccessfulValidatorForUser(username);
            var dbContext = DbContext(username, "refreshToken");
            var factory = TokenFactory("test-access-token");

            var result = Service(validator, factory, dbContext)
                .RenewAccess("accessToken", "refreshToken");

            result.Success.Should().BeTrue();
            result.RefreshToken.Should().BeEquivalentTo("refreshToken");
        }

        private IAccessTokenFactory TokenFactory(string accessToken)
        {
            var tokenFactory = new Mock<IAccessTokenFactory>();
            tokenFactory.Setup(x => x.Create("user@notablog.com"))
                .Returns(new AccessToken
                {
                    Token = accessToken,
                    Expires = DateTime.Now.AddHours(2)
                });

            return tokenFactory.Object;
        }

        private ISecurityTokenValidator FailingValidator()
        {
            var mock = new Mock<ISecurityTokenValidator>();
            SecurityToken token;
            mock.Setup(x => x.ValidateToken(It.IsAny<string>(), It.IsAny<TokenValidationParameters>(), out token))
                .Throws<SecurityTokenException>();
            return mock.Object;
        }

        private ISecurityTokenValidator SuccessfulValidatorForUser(string username)
        {
            var claims = new FakeClaimsPrincipal(new Claim("sub", username));
            return SuccessfulValidator(claims);
        }

        private ISecurityTokenValidator SuccessfulValidator(ClaimsPrincipal claims)
        {
            var mock = new Mock<ISecurityTokenValidator>();
            SecurityToken token;
            mock.Setup(x => x.ValidateToken(It.IsAny<string>(), It.IsAny<TokenValidationParameters>(), out token))
                .Returns(claims);
            return mock.Object;
        }

        private RenewAccessService Service(ISecurityTokenValidator validator = null, 
            IAccessTokenFactory factory = null, ApplicationDbContext applicationDbContext = null)
        {
            var tokenConfiguration = new TokenConfiguration
            {
                Audience = "Notablog",
                Issuer = "Notablog",
                Key = "0123456789876543210",
                ValidForMinutes = 5
            };
            return new RenewAccessService(tokenConfiguration, factory, validator, applicationDbContext);
        }

        private ApplicationDbContext DbContext(string username = null, string refreshToken = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var db = new ApplicationDbContext(optionsBuilder.Options);
            
            if (!string.IsNullOrEmpty(username))
            {
                db.Users.Add(new ApplicationUser
                {
                    UserName = username
                });
            }

            if (!string.IsNullOrEmpty(refreshToken))
            {
                db.RefreshTokens.Add(new RefreshToken
                {
                    UserId = username,
                    Token = refreshToken
                });
            }

            db.SaveChanges();

            return db;
        }
    }
}
