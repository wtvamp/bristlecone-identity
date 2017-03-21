using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HoradricCube.Entities.Base;

namespace Bristlecone.Auth.Identity
{
    /// <summary>
    /// Generic Bristlecone User with no associated entities from the IDS Database
    /// </summary>
    public class BristleconeUser : ApplicationUser
    {
        /// <summary>
        /// Creates new ClaimsIdentity for a Generic Bristlecone User with no associated entities from the IDS Database
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<BristleconeUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
