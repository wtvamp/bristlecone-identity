using System.Threading.Tasks;
using FluentAssertions;
using Bristlecone.Auth.Identity;
using Moq;
using NUnit.Framework;
using HoradricCube.DbContexts;

namespace Bristlecone.Auth.IdentityTests
{
    [TestFixture]
    public class BristleconeUserStoreTests
    {
        private Mock<ApplicationDbContext> _authContext;

        [SetUp]
        public void Init()
        {
            var authContextHelper = new MockAuthContext();
            _authContext = authContextHelper.GetMockAuthContext();
        }

        [Test]
        public async Task Given_an_BristleconeUserStore_when_a_user_is_created_then_it_can_be_retrieved_by_username()
        {
            // GIVEN a user store and user
            var userStore = new BristleconeUserStore(_authContext.Object);

            var user = new BristleconeUser
            {
                UserName = "TestUser3",
                Email = "TestUser3@test.com"                
            };

            // WHEN the user is created
            await userStore.CreateAsync(user, false);

            // THEN the user should retrieved from the user store and match the original user
            var userFromStore = await userStore.FindByNameAsync(user.UserName, false);

            userFromStore.UserName.Should().Be(user.UserName, "because the user has been created in and retrieved from the BristleconeUserStore.");
        }

        [Test]
        public async Task Given_an_BristleconeUserStore_when_a_user_is_created_then_it_can_be_retrieved_by_userid()
        {
            // GIVEN a user store and user
            var userStore = new BristleconeUserStore(_authContext.Object);

            var user = new BristleconeUser
            {
                Id ="TestUser3",
                UserName = "TestUser3",
                Email = "TestUser3@test.com"
            };

            // WHEN the user is created
            await userStore.CreateAsync(user, false);

            // THEN the user should retrieved from the user store and match the original user
            var userFromStore = await userStore.FindByIdAsync(user.Id, false);

            userFromStore.Id.Should().Be(user.Id, "because the user has been created in and retrieved from the BristleconeUserStore.");
        }

        [Test]
        public async Task Given_an_BristleconeUserStore_when_a_user_is_found_by_username_then_it_calls_base()
        {
            // GIVEN a user store and user
            var userStore = new Mock<BristleconeUserStore>(_authContext.Object)
            {
                CallBase = true
            };

            var user = new BristleconeUser
            {
                UserName = "TestUser3",
                Email = "TestUser3@test.com"
            };

            userStore.Setup(m => m.BaseFindByNameAsync(user.UserName)).Returns(Task.FromResult(user));

            // WHEN the user is created
            await userStore.Object.FindByNameAsync(user.UserName);

            // THEN the BaseCreateAsync method should have been called
            userStore.Verify(m => m.BaseFindByNameAsync(user.UserName), Times.Once);
        }

        [Test]
        public async Task Given_an_BristleconeUserStore_when_a_user_is_found_by_userid_then_it_calls_base()
        {
            // GIVEN a user store and user
            var userStore = new Mock<BristleconeUserStore>(_authContext.Object)
            {
                CallBase = true
            };

            var user = new BristleconeUser
            {
                Id = "TestUser3",
                UserName = "TestUser3",
                Email = "TestUser3@test.com"
            };

            userStore.Setup(m => m.BaseFindByIdAsync(user.Id)).Returns(Task.FromResult(user));

            // WHEN the user is created
            await userStore.Object.FindByIdAsync(user.Id);

            // THEN the BaseCreateAsync method should have been called
            userStore.Verify(m => m.BaseFindByIdAsync(user.Id), Times.Once);
        }

        [Test]
        public async Task Given_an_BristleconeUserStore_when_a_user_is_updated_then_it_calls_base()
        {
            // GIVEN a user store and user
            var userStore = new Mock<BristleconeUserStore>(_authContext.Object)
            {
                CallBase = false
            };
            
            var user = new BristleconeUser
            {
                UserName = "TestUser3",
                Email = "TestUser3@test.com"
            };
            
            userStore.Setup(m => m.BaseUpdateAsync(user)).Returns(Task.FromResult(user));

            // WHEN the user is created
            await userStore.Object.UpdateAsync(user, true);

            // THEN the BaseCreateAsync method should have been called
            userStore.Verify(m => m.BaseUpdateAsync(user), Times.Once);
        }

        [Test]
        public async Task Given_an_BristleconeUserStore_when_a_user_is_deleted_then_it_calls_base()
        {
            // GIVEN a user store and user
            var userStore = new Mock<BristleconeUserStore>(_authContext.Object)
            {
                CallBase = false
            };

            var user = new BristleconeUser
            {
                UserName = "TestUser3",
                Email = "TestUser3@test.com"
            };

            userStore.Setup(m => m.BaseDeleteAsync(user)).Returns(Task.FromResult(user));

            // WHEN the user is deleted
            await userStore.Object.DeleteAsync(user, true);

            // THEN the BaseDeleteAsync method should have been called
            userStore.Verify(m => m.BaseDeleteAsync(user), Times.Once);
        }

        [Test]
        public async Task Given_an_BristleconeUserStore_when_a_user_is_created_then_it_calls_base()
        {
            // GIVEN a user store and user
            var userStore = new Mock<BristleconeUserStore>(_authContext.Object)
            {
                CallBase = false
            };

            var user = new BristleconeUser
            {
                UserName = "TestUser3",
                Email = "TestUser3@test.com"
            };

            userStore.Setup(m => m.BaseCreateAsync(user)).Returns(Task.FromResult(user));

            // WHEN the user is created
            await userStore.Object.CreateAsync(user, true);

            // THEN the BaseCreateAsync method should have been called
            userStore.Verify(m => m.BaseCreateAsync(user), Times.Once);
        }

        [Test]
        public async Task Given_an_BristleconeUserStore_when_a_user_is_updated_then_it_can_be_retrieved()
        {
            // Given an BristleconeUserStore and BristleconeUser
            var userStore = new BristleconeUserStore(_authContext.Object);
            var user = new BristleconeUser
            {
                UserName = "TestUser5",
                Email = "TestUser5@test.com"
            };

            // WHEN creating and upating a user.
            await userStore.CreateAsync(user, false);
            var userFromStore = await userStore.FindByNameAsync(user.UserName, false);
            userFromStore.UserName.Should().Be(user.UserName, "because the user has been created in and retrieved from the userstore.");

            // THEN the user should be the same in the store after being updated.
            await userStore.UpdateAsync(user, false);
            userFromStore.UserName.Should().Be(user.UserName, "because the user has been updated in the userstore.");
            await userStore.DeleteAsync(user, false);
        }

        [TearDown]
        public void TestTearDown()
        {
            _authContext = null;
        }
    }
}