using System.Linq;
using System.Security.Claims;
using AspNetCoreAssessment2.Foundation.Identity;

namespace AspNetCoreAssessment2.Foundation.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasClaim(this ClaimsPrincipal claimsPrincipal, string type, params string[] values)
        {
            return values.Any(v => claimsPrincipal.HasClaim(type, v));
        }

        public static bool HasReadRules(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(AdditionalUserClaims.HasReadRules, AdditionalUserClaims.AllowedHasReadRulesValues.ToArray());
        }
    }
}