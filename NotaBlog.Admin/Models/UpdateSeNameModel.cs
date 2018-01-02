using System.ComponentModel.DataAnnotations;

namespace NotaBlog.Admin.Models
{
    public class UpdateSeNameModel
    {
        [Required]
        public string SeName { get; set; }
    }
}
