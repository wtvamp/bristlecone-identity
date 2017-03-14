using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bristlecone.Auth.Identity
{
    /// <summary>
    /// IDExperts Role Manager for managing ID Experts roles
    /// </summary>
    public class BristleconeRoleManager : RoleManager<IdentityRole>
    {
        /// <summary>
        /// Creates new instance of IDExperts Role Manager for managing ID Experts roles
        /// </summary>
        /// <param name="roleStore"></param>
        public BristleconeRoleManager(BristleconeRoleStore roleStore) : base(roleStore)
        {

        }
    }
}
