using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using HoradricCube.DbContexts;

namespace Bristlecone.Auth.Identity
{
    /// <summary>
    /// IDExperts User Manager for handling IDExperts users with no associated IDS entities
    /// </summary>
    public class BristleconeUserManager : UserManager<BristleconeUser>
    {
        /// <summary>
        /// Injector used by Owin for injecting IUserStore with type BristleconeUser
        /// </summary>
        /// <param name="store"></param>
        /// Excluded because there is no logic and it is not used by our application
        [ExcludeFromCodeCoverage]
        public BristleconeUserManager(IUserStore<BristleconeUser> store) : base(store)
        {
        }

        /// <summary>
        /// Injector used by Owin for injecting IUserStore with type BristleconeUser
        /// </summary>
        /// <param name="store"></param>>
        public BristleconeUserManager(BristleconeUserStore store) : base(store)
        {
        }

        /// <summary>
        /// Creates a new ID Experts UserManager with no associated IDS Entities
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns>BristleconeUserManager for managing ID Experts with no associated IDS Entities</returns>
        public static BristleconeUserManager Create(IdentityFactoryOptions<BristleconeUserManager> options, IOwinContext context)
        {
            var manager = new BristleconeUserManager(new BristleconeUserStore(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<BristleconeUser>(manager)
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
                manager.UserTokenProvider = new DataProtectorTokenProvider<BristleconeUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
