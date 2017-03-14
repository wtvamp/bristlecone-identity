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
    public class BristleconeUserStore: UserStore<BristleconeUser>
    {
        private BristleconeAuthDbContext _authContext;

        /// <summary>
        /// Bristlecone User store with no connected DB tables
        /// </summary>
        /// <param name="authContext">User DbContext</param>
        public BristleconeUserStore(BristleconeAuthDbContext authContext) : base(authContext)
        {
            _authContext = authContext;
        }


        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="user"></param>
        [ExcludeFromCodeCoverage]
        public override Task CreateAsync(BristleconeUser user) => CreateAsync(user, true);

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="user"></param>
        [ExcludeFromCodeCoverage]
        public virtual Task BaseCreateAsync(BristleconeUser user) => base.CreateAsync(user);

        /// <summary>
        /// Creates a new user and a corresponding Participent record
        /// </summary>
        /// <param name="user"></param>
        /// <param name="callBase"></param>
        /// <returns></returns>
        public Task CreateAsync(BristleconeUser user, bool callBase) { 
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
        public override Task UpdateAsync(BristleconeUser user) => UpdateAsync(user, true);

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="user"></param>
        [ExcludeFromCodeCoverage]
        public virtual Task BaseUpdateAsync(BristleconeUser user) => base.UpdateAsync(user);

        /// <summary>
        /// Updates an Bristlecone User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="callBase"></param>
        /// <returns></returns>
        public Task UpdateAsync(BristleconeUser user, bool callBase)
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
        public override Task DeleteAsync(BristleconeUser user) => DeleteAsync(user, true);


        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="user"></param>
        [ExcludeFromCodeCoverage]
        public virtual Task BaseDeleteAsync(BristleconeUser user) => base.DeleteAsync(user);


        /// <summary>
        /// Deletes a user and it's associated participant record from the database
        /// </summary>
        /// <param name="user" type="ParticipantUser">The user to be deleted</param>
        /// <param name="callBase"></param>
        /// <returns></returns>
        public Task DeleteAsync(BristleconeUser user, bool callBase)
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
        public override async Task<BristleconeUser> FindByIdAsync(string userId) => await FindByIdAsync(userId, true);


        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="userId"></param>
        [ExcludeFromCodeCoverage]
        public virtual Task<BristleconeUser> BaseFindByIdAsync(string userId) => base.FindByIdAsync(userId);


        /// <summary>
        /// Finds user by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="callBase"></param>
        /// <returns></returns>
        public async Task<BristleconeUser> FindByIdAsync(string userId, bool callBase)
        {
            BristleconeUser user;
            if (callBase)
            {
                user = await BaseFindByIdAsync(userId);
            }
            else
            {
                user = _authContext.Users.FirstOrDefault(e => e.Id == userId);
            }
            return user;
        }

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="userName"></param>
        [ExcludeFromCodeCoverage]
        public override async Task<BristleconeUser> FindByNameAsync(string userName) => await FindByNameAsync(userName, true);

        /// <summary>
        /// Excluded because base of Identity objects are not mockable
        /// </summary>
        /// <param name="userName"></param>
        [ExcludeFromCodeCoverage]
        public virtual Task<BristleconeUser> BaseFindByNameAsync(string userName) => base.FindByNameAsync(userName);

        /// <summary>
        /// Find an Bristlecone User by username search
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="callBase"></param>
        /// <returns></returns>
        public async Task<BristleconeUser> FindByNameAsync(string userName, bool callBase)
        {
            BristleconeUser user;
            if (callBase)
            {
                user = await BaseFindByNameAsync(userName);
            }
            else
            {
                user = _authContext.Users.FirstOrDefault(e => e.UserName == userName);
            }
            return user;
        }
    }
}
