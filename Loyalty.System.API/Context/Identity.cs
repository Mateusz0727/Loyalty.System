using System.Security.Claims;
using System.Security.Principal;

namespace Loyalty.System.API.Context
{
    public static class Identity
    {
        #region Id()
        public static string Id(this IPrincipal principal)
        {
            return (principal as ClaimsPrincipal).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string Id(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        #endregion
    }
}
