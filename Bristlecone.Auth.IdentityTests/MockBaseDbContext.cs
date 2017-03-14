using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Moq;

namespace Bristlecone.Test
{
    /// <summary>
    /// Mock for Base Db Context that provides helpers for generating context items like DbSets
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MockBaseDbContext
    {
        /// <summary>
        /// Helper class to generate a mocked DbContext for a given type
        /// </summary>
        /// <typeparam name="T">A BaseEntity with which to use to mock a DbSet with a List as the backing in-memory storage</typeparam>
        /// <param name="list">A list to use as the in-memory storage container</param>
        /// <returns>A mocked DbSet of Type T where T is a BaseEntity</returns>
        /// Excluding from coverage because it adds helper methods that aren't used yet.  The ones that are used are covered
        [ExcludeFromCodeCoverage]
        protected Mock<DbSet<T>> MockDbSet<T>(List<T> list) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(() => list.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(() => list.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(() => list.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => list.GetEnumerator());
            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback((T x) => list.Add(x)).Returns<T>(a => a);
            mockSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<T>>())).Callback((IEnumerable<T> x) => list.AddRange(x));
            mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback((T x) => list.Remove(x));
            mockSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<T>>())).Callback((IEnumerable<T> x) => list.RemoveAll(x.Contains));
            return mockSet;
        }
    }
}
