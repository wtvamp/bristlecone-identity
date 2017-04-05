using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using HoradricCube.Entities.Base;

namespace Bristlecone.Auth.Identity
{
    /// <summary>
    /// Bristlecone Sign In Manager for generating users
    /// </summary>
    /// Excluded from coverage because it's not used anywhere in Bristlecone Custom Code 
    /// I suspect it is just a requirement for the OWIN middleware
    [ExcludeFromCodeCoverage]
    public class BristleconeSignInManager : SignInManager<ApplicationUser, string>
    {
        /// <summary>
        /// Creates new instance of Bristlecone Sign In Manager for generating users
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="authenticationManager"></param>
        public BristleconeSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        /// <summary>
        /// Creates a new ClaimsIdentity for an IDExperts User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager, DefaultAuthenticationTypes.ApplicationCookie);
        }

        /// <summary>
        /// Creates a new IDExperts SignInManager
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static BristleconeSignInManager Create(IdentityFactoryOptions<BristleconeSignInManager> options, IOwinContext context)
        {
            return new BristleconeSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
