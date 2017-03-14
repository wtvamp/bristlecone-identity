using Microsoft.AspNet.Identity.EntityFramework;

namespace Bristlecone.Auth.Identity
{
    /// <summary>
    /// IDExperts Role Store for storing IDExperts roles
    /// </summary>
    public class BristleconeRoleStore : RoleStore<IdentityRole>
    {
        /// <summary>
        /// Creates new instance of IDExperts Role Store for storing IDExperts roles
        /// </summary>
        /// <param name="ctx"></param>
        public BristleconeRoleStore(BristleconeAuthDbContext ctx) : base(ctx)
        {

        }
    }
}
