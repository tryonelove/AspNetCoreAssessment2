using System.ComponentModel.DataAnnotations;

namespace AspNetCoreAssessment2.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember")]
        public bool ShouldRemember { get; set; }
    }
}