using HoradricCube.DbContexts;
using HoradricCube.Entities.Base;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Bristlecone.Auth.Identity
{
    /// <summary>
    /// Creates a new Bristlecone Participant user store
    /// </summary>
    public class ApplicationUserStore: UserStore<ApplicationUser>
    {
        private BristleconeAuthDbContext _authContext;

        /// <summary>
        /// Bristlecone User store with no connected DB tables
        /// </summary>
        /// <param name="authContext">User DbContext</param>
        public ApplicationUserStore(BristleconeAuthDbContext authContext) : base(authContext)
        {
            _authContext = authContext;
        }


        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="user"></param>
        [ExcludeFromCodeCoverage]
        public override Task CreateAsync(ApplicationUser user) => CreateAsync(user, true);

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="user"></param>
        [ExcludeFromCodeCoverage]
        public virtual Task BaseCreateAsync(ApplicationUser user) => base.CreateAsync(user);

        /// <summary>
        /// Creates a new user and a corresponding Participent record
        /// </summary>
        /// <param name="user"></param>
        /// <param name="callBase"></param>
        /// <returns></returns>
        public Task CreateAsync(ApplicationUser user, bool callBase) { 
            if (callBase)
            {
                return BaseCreateAsync(user);
            }
            else
            {
                _authContext.Users.Add(user);
                _authContext.SaveChanges();
                return Task.Delay(0);
            }
        }

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="user"></param>
        [ExcludeFromCodeCoverage]
        public override Task UpdateAsync(ApplicationUser user) => UpdateAsync(user, true);

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="user"></param>
        [ExcludeFromCodeCoverage]
        public virtual Task BaseUpdateAsync(ApplicationUser user) => base.UpdateAsync(user);

        /// <summary>
        /// Updates an Bristlecone User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="callBase"></param>
        /// <returns></returns>
        public Task UpdateAsync(ApplicationUser user, bool callBase)
        {
            if (callBase)
            {
                return BaseUpdateAsync(user);
            }
            else
            {
                _authContext.Users.Attach(user);
                _authContext.SetState(user, EntityState.Modified);
                _authContext.SaveChanges();
                return Task.Delay(0);
            }
        }

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="user"></param>
        [ExcludeFromCodeCoverage]
        public override Task DeleteAsync(ApplicationUser user) => DeleteAsync(user, true);


        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="user"></param>
        [ExcludeFromCodeCoverage]
        public virtual Task BaseDeleteAsync(ApplicationUser user) => base.DeleteAsync(user);


        /// <summary>
        /// Deletes a user and it's associated participant record from the database
        /// </summary>
        /// <param name="user" type="ParticipantUser">The user to be deleted</param>
        /// <param name="callBase"></param>
        /// <returns></returns>
        public Task DeleteAsync(ApplicationUser user, bool callBase)
        {
            if (callBase)
            {
                return BaseDeleteAsync(user);
            }
            else
            {
                _authContext.Users.Remove(user);
                _authContext.SaveChanges();
                return Task.Delay(0);
            }
        }

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="userId"></param>
        [ExcludeFromCodeCoverage]
        public override async Task<ApplicationUser> FindByIdAsync(string userId) => await FindByIdAsync(userId, true);


        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="userId"></param>
        [ExcludeFromCodeCoverage]
        public virtual Task<ApplicationUser> BaseFindByIdAsync(string userId) => base.FindByIdAsync(userId);


        /// <summary>
        /// Finds user by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="callBase"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindByIdAsync(string userId, bool callBase)
        {
            ApplicationUser user;
            if (callBase)
            {
                user = await BaseFindByIdAsync(userId);
            }
            else
            {
                user = _authContext.Users.FirstOrDefault(e => e.Id == userId);
            }
            return (ApplicationUser)user;
        }

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="userName"></param>
        [ExcludeFromCodeCoverage]
        public override async Task<ApplicationUser> FindByNameAsync(string userName) => await FindByNameAsync(userName, true);

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="userName"></param>
        [ExcludeFromCodeCoverage]
        public virtual Task<ApplicationUser> BaseFindByNameAsync(string userName) => base.FindByNameAsync(userName);

        /// <summary>
        /// Find an Bristlecone User by username search
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="callBase"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindByNameAsync(string userName, bool callBase)
        {
            ApplicationUser user;
            if (callBase)
            {
                user = await BaseFindByNameAsync(userName);
            }
            else
            {
                user = _authContext.Users.FirstOrDefault(e => e.UserName == userName);
            }
            return (ApplicationUser)user;
        }
    }
}
