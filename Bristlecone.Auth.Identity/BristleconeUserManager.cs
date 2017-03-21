using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using HoradricCube.DbContexts;
using HoradricCube.Entities.Base;

namespace Bristlecone.Auth.Identity
{
    /// <summary>
    /// IDExperts User Manager for handling IDExperts users with no associated IDS entities
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Injector used by Owin for injecting IUserStore with type ApplicationUser
        /// </summary>
        /// <param name="store"></param>
        /// Excluded because there is no logic and it is not used by our application
        [ExcludeFromCodeCoverage]
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        /// <summary>
        /// Injector used by Owin for injecting IUserStore with type ApplicationUser
        /// </summary>
        /// <param name="store"></param>>
        public ApplicationUserManager(ApplicationUserStore store) : base(store)
        {
        }

        /// <summary>
        /// Creates a new ID Experts UserManager with no associated IDS Entities
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns>ApplicationUserManager for managing ID Experts with no associated IDS Entities</returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new ApplicationUserStore(context.Get<BristleconeAuthDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
