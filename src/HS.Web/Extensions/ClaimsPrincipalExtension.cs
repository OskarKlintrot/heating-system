using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HS.Web.Extensions
{
    /// <summary>
    /// Collection of helper methods for easy retrival of claims
    /// </summary>
    public static class ClaimsPrincipalExtension
    {
        // Bodge since IOption needs DI and therefore can't be used in a static class
        public static string AdminGroupId { get; set; } = String.Empty;

        /// <summary>
        /// Returns true if the user is Admin
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims
                .Where(c => c.Type.Equals("groups"))
                .Select(c => c.Value)
                .Contains(AdminGroupId);
        }

        /// <summary>
        /// Get the username
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims
                .SingleOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?
                .Value;
        }

        /// <summary>
        /// Get the display name
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static string GetDisplayName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims
                .SingleOrDefault(c => c.Type.Equals("name"))?
                .Value;
        }

        /// <summary>
        /// Get the display name
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims
                .SingleOrDefault(c => c.Type.Equals(ClaimTypes.Email))?
                .Value;
        }
    }
}
