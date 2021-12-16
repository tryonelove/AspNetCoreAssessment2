using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreAssessment2.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AspNetCoreAssessment2.Foundation.Identity
{
    public class ReadRulesClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public ReadRulesClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> options)
            : base(userManager, options)
        {

        }


        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            identity.AddClaim(new Claim(AdditionalUserClaims.HasReadRules, user.HasReadRules.ToString()));

            return principal;
        }
    }
}