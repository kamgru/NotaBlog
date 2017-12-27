using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NotaBlog.Admin.Models;
using System.Threading.Tasks;

namespace NotaBlog.Admin.Tests
{
    public class FakeSignInManager : SignInManager<ApplicationUser>
    {
        public SignInResult PasswordSignInAsyncResult { get; set; }

        public FakeSignInManager()
            : base(
                  new FakeUserManager(), 
                  new Mock<IHttpContextAccessor>().Object, 
                  new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object, 
                  new Mock<IOptions<IdentityOptions>>().Object, 
                  new Mock<ILogger<SignInManager<ApplicationUser>>>().Object, 
                  new Mock<IAuthenticationSchemeProvider>().Object)
        {
        }

        public override Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return Task.FromResult(PasswordSignInAsyncResult);
        }
    }
}
