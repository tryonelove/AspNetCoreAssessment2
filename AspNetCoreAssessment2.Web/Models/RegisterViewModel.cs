using System.ComponentModel.DataAnnotations;

namespace AspNetCoreAssessment2.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool HasReadRules { get; set; }
    }
}