using System.Collections.Generic;

namespace AspNetCoreAssessment2.Foundation.Identity
{
    public static class AdditionalUserClaims
    {
        public const string HasReadRules = "HasReadRules";

        public static readonly IReadOnlyCollection<string> AllowedHasReadRulesValues = new [] { true.ToString() };
    }
}