using NotaBlog.Admin.Models;

namespace NotaBlog.Admin.Services
{
    public class RenewAccessResult
    {
        public bool Success { get; set; }
        public AccessToken AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public RenewAccessResult(bool success) => Success = success;

    }
}
