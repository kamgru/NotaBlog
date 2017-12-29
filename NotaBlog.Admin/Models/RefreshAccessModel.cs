using System.ComponentModel.DataAnnotations;

namespace NotaBlog.Admin.Models
{
    public class RefreshAccessModel
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
