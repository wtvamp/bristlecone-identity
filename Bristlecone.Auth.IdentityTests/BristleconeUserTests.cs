using System.Collections.Generic;
using System.Security.Claims;
using Bristlecone.Auth.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;
using HoradricCube.DbContexts;

namespace Bristlecone.Auth.IdentityTests
{
    [TestFixture]
    public class BristleconeUserTests
    {
        private Mock<ApplicationDbContext> _authContext;

        [SetUp]
        public void Init()
        {
            var authContextHelper = new MockAuthContext();
            _authContext = authContextHelper.GetMockAuthContext();
        }

        [Test]
        public async Task Given_an_BristleconeUserManager_when_asking_for_a_claimsidentity_then_claimsidentity_is_returned()
        {
            // GIVEN an BristleconeUserManager, BristleconeUserStore, and BristleconeUser
            var userStore = new Mock<BristleconeUserStore>(_authContext.Object);
            var userManager = new Mock<BristleconeUserManager>(userStore.Object);

            userManager.Setup(m => m.CreateAsync(It.IsAny<BristleconeUser>())).ReturnsAsync(() => IdentityResult.Success);
            userManager.Setup(m => m.DeleteAsync(It.IsAny<BristleconeUser>())).ReturnsAsync(() => IdentityResult.Success);
            userManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new BristleconeUser()
            {
                Id = "TestUser5",
                UserName = "TestUser5",
                Email = "TestUser5@test.com"
            });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "TestUser5"),
                new Claim(ClaimTypes.Email, "TestUser5@test.com")
            };
            var claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ExternalBearer);

            userManager.Setup(m => m.CreateIdentityAsync(It.IsAny<BristleconeUser>(), It.IsAny<string>()))
                .ReturnsAsync(claimsIdentity);

            var user = new BristleconeUser
            {
                Id = "TestUser5",
                UserName = "TestUser5",
                Email = "TestUser5@test.com"
            };

            // WHEN the user manager creates the user and requests an idenity
            await userManager.Object.CreateAsync(user);
            var oAuthIdentity = user.GenerateUserIdentityAsync(userManager.Object, OAuthDefaults.AuthenticationType).Result;

            // THEN the returned oauth identity should have the same username as the user
            oAuthIdentity.GetUserName().Should().Be("TestUser5", "because the oauth identity was created and retrieved from the claim.");
            await userManager.Object.DeleteAsync(user);
        }

        [TearDown]
        public void TestTearDown()
        {
            _authContext = null;
        }
    }
}