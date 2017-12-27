using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NotaBlog.Admin.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NotaBlog.Admin.Tests
{
    public class FakeUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUser FindByEmailAsyncResult { get; set; }

        public FakeUserManager()
            : base(
                  new Mock<IUserStore<ApplicationUser>>().Object, 
                  new Mock<IOptions<IdentityOptions>>().Object, 
                  new Mock<IPasswordHasher<ApplicationUser>>().Object, 
                  Enumerable.Empty<IUserValidator<ApplicationUser>>(), 
                  Enumerable.Empty<IPasswordValidator<ApplicationUser>>(), 
                  new Mock<ILookupNormalizer>().Object, 
                  new Mock<IdentityErrorDescriber>().Object, 
                  new Mock<IServiceProvider>().Object, 
                  new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        {
        }

        public override Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return Task.FromResult(FindByEmailAsyncResult);
        }
    }
}
