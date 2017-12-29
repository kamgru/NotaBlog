using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace NotaBlog.Admin.Tests
{
    public class FakeIdentity: ClaimsIdentity
    {
        public FakeIdentity(params Claim[] claims) : base(claims) { }
    }

    public class FakeClaimsPrincipal : ClaimsPrincipal
    {
        public FakeClaimsPrincipal(params Claim[] claims) : base(new FakeIdentity(claims)) { }
    }
}
