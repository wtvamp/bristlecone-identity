using NUnit.Framework;
using Bristlecone.Auth.Identity;
using FluentAssertions;
using Moq;
using HoradricCube.DbContexts;

namespace Bristlecone.Auth.IdentityTests
{
    [TestFixture]
    public class BristleconeRoleManagerTests
    {
        private Mock<ApplicationDbContext> _authContext;
        private Mock<BristleconeRoleStore> _BristleconeRoleStore;

        [SetUp]
        public void init()
        {
            var authContextHelper = new MockAuthContext();
            _authContext = authContextHelper.GetMockAuthContext();
            _BristleconeRoleStore = new Mock<BristleconeRoleStore>(_authContext.Object);

        }

        [Test]
        public void Given_an_BristleconeRoleStore_when_the_BristleconeRoleManager_constructor_is_called_then_a_new_instance_is_created()
        {
            // GIVEN an Bristlecone RoleStore
            // WHEN the constructor is called
            var BristleconeRoleManager = new BristleconeRoleManager(_BristleconeRoleStore.Object);
            // THEN a new instance of BristleconeRoleManager is created
            BristleconeRoleManager.Should().BeOfType<BristleconeRoleManager>("because we called the constructor ");
        }
    }
}