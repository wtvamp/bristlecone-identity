using Bristlecone.Auth.Identity;
using Bristlecone.Test;
using HoradricCube.DbContexts;
using HoradricCube.Entities.Base;
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

            var mockApplicationUserSet = MockDbSet(new List<ApplicationUser>());
            authContext.Setup(m => m.Users).Returns(mockApplicationUserSet.Object);
            //authContext.Setup(m => m.GetState(It.IsAny<ApplicationUser>()));
            //authContext.Setup(m => m.SetState(It.IsAny<ApplicationUser>(), It.IsAny<EntityState>()));
            return authContext;
        }
    }
}
