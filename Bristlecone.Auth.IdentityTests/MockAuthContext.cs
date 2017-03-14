using Bristlecone.Auth.Identity;
using Bristlecone.Test;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace Bristlecone.Auth.IdentityTests
{
    [ExcludeFromCodeCoverage]
    public class MockAuthContext : MockBaseDbContext
    {
        public Mock<BristleconeAuthDbContext> GetMockAuthContext()
        {
            var authContext = new Mock<BristleconeAuthDbContext>();

            var mockBristleconeUserSet = MockDbSet(new List<BristleconeUser>());
            authContext.Setup(m => m.Users).Returns(mockBristleconeUserSet.Object);
            authContext.Setup(m => m.GetState(It.IsAny<BristleconeUser>()));
            authContext.Setup(m => m.SetState(It.IsAny<BristleconeUser>(), It.IsAny<EntityState>()));
            return authContext;
        }
    }
}
