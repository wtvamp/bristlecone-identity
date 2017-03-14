using Bristlecone.Auth.Identity;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace Bristlecone.Auth.IdentityTests
{
    [ExcludeFromCodeCoverage]
    public class MockAuthContext : MockBaseDbContext
    {
        public Mock<BristleconeEmployeeAuthDbContext> GetMockEmployeeAuthContext()
        {
            var authContext = new Mock<BristleconeEmployeeAuthDbContext>();

            var mockBristleconeEmployeeUserSet = MockDbSet(new List<BristleconeEmployeeUser>());
            authContext.Setup(m => m.Users).Returns(mockBristleconeEmployeeUserSet.Object);
            authContext.Setup(m => m.GetState(It.IsAny<BristleconeEmployeeUser>()));
            authContext.Setup(m => m.SetState(It.IsAny<BristleconeEmployeeUser>(), It.IsAny<EntityState>()));
            return authContext;
        }

        public Mock<BristleconeParticipantAuthDbContext> GetMockParticipantAuthContext()
        {
            var authContext = new Mock<BristleconeParticipantAuthDbContext>();

            var mockBristleconeParticipantUserSet = MockDbSet(new List<BristleconeParticipantUser>());
            authContext.Setup(m => m.Users).Returns(mockBristleconeParticipantUserSet.Object);
            authContext.Setup(m => m.GetState(It.IsAny<BristleconeParticipantUser>()));
            authContext.Setup(m => m.SetState(It.IsAny<BristleconeParticipantUser>(), It.IsAny<EntityState>()));
            return authContext;
        }

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
