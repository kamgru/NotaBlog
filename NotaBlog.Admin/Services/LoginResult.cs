using NotaBlog.Admin.Models;

namespace NotaBlog.Admin.Services
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public AccessToken AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public LoginResult(bool success) => Success = success;
    }
}
