using Microsoft.AspNetCore.Identity;

namespace AspNetCoreAssessment2.DomainModel
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public bool HasReadRules { get; set; }
    }
}