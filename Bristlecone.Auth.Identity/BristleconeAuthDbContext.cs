using HoradricCube.DbContexts;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace Bristlecone.Auth.Identity
{
    /// <summary>
    /// Auth Context for DbEntities associated with IDExperts Auth Database
    /// </summary>
    public class BristleconeAuthDbContext : ApplicationDbContext
    {
        /// <summary>
        /// Create new Auth Context for DbEntities associated with IDExperts Auth Database
        /// </summary>
        public BristleconeAuthDbContext() : base("BristleconeIdentity")
        {
        }

        /// <summary>
        /// Create new Auth Context for DbEntities associated with IDExperts Auth Database
        /// </summary>
        /// <returns>New Auth Context for DbEntities associated with IDExperts Auth Database</returns>
        public static BristleconeAuthDbContext Create()
        {
            return new BristleconeAuthDbContext();
        }

        /// <summary>
        /// Sets the state on a particular type - used to wrap Entry since Entry is not virtual and can't be mocked
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="state"></param>
        /// Excluded from coverage because it is a wrapper/helper function and Entry can't be mocked
        [ExcludeFromCodeCoverage]
        public virtual void SetState<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            Entry(entity).State = state;
        }

        /// <summary>
        /// Gets the state on a particular type - used to wrap Entry since Entry is not virtual and can't be mocked
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// Excluded from coverage because it is a wrapper/helper function and Entry can't be mocked
        [ExcludeFromCodeCoverage]
        public virtual EntityState GetState<TEntity>(TEntity entity) where TEntity : class
        {
            return Entry(entity).State;
        }
    }
}
