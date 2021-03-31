using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace PM_Audit.Models.Authorization_Models
{
    public static class ClaimsUtility
    {
        public static object CustomClaimTypes { get; private set; }

        public static Claim GetClaimValue(this IIdentity identity, string CustomClaimType)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaimType);
            return claim;
        }
        public static bool HasPermission(this IIdentity identity, string Permission)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaims.Permissions);
            if (claim != null)
            {
                var permissions = new List<string>(claim.Value.ToString().Split(','));
                return permissions.Contains(Permission);
            }
            else
            {
                return false;
            }
        }
        public static bool IsJazzUser(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaims.IsJazz);
            if (claim != null)
            {
                var isJazz = claim.Value;
                return isJazz == "True";
            }
            else
            {
                return false;
            }
        }
        public static List<string> GetPermissionsList(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaims.Permissions);
            if (claim != null)
            {
                var claimValue = claim.Value.ToString();
                return new List<string>(claimValue.Split(','));
            }
            else
                return new List<string>();
        }


    }
    public static class CustomClaims
    {
        public const string Sid = ClaimTypes.Sid;
        public const string Name = ClaimTypes.Name;
        public const string FriendlyName = "FriendlyName";
        public const string UserName = "UserName";
        public const string Email = ClaimTypes.Email;
        public const string IsJazz = "IsJazz";
        public const string Permissions = "Permissions";
    }
}