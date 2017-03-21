using FluentAssertions;
using NUnit.Framework;
using Bristlecone.Auth.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Moq;
using HoradricCube.DbContexts;
using HoradricCube.Entities.Base;

namespace Bristlecone.Auth.IdentityTests
{
    [TestFixture]
    public class ApplicationUserManagerTests
    {
        private Mock<BristleconeAuthDbContext> _authContext;

        [SetUp]
        public void Init()
        {
            var authContextHelper = new MockAuthContext();
            _authContext = authContextHelper.GetMockAuthContext();
        }

        [Test]
        public void Given_an_ApplicationUserManager_when_a_user_is_created_then_identity_properties_like_UserValidator_are_available()
        {
            // GIVEN a mocked IOwinContext and new ApplicationUserStore/ApplicationUserManager
            var _mockAppBuilder = new Mock<IOwinContext>();
            _mockAppBuilder.Setup(f => f.Get<BristleconeAuthDbContext>(It.IsAny<string>())).Returns(_authContext.Object);

            var store = new ApplicationUserStore(_authContext.Object);
            var userManager = new ApplicationUserManager(store);

            var identifyFactoryOptions = new IdentityFactoryOptions<ApplicationUserManager>
            {
                DataProtectionProvider = new DpapiDataProtectionProvider()
            };

            // WHEN a user is created using the ApplicationUserManager
            ApplicationUserManager.Create(identifyFactoryOptions, _mockAppBuilder.Object);

            // THEN the correct user is created with a UserTokerProvider, UserValidator, and PasswordValidator
            userManager.UserValidator.Should().BeOfType<UserValidator<ApplicationUser, string>>("because it has be created using the ApplicationUserManager with correct IdentityFactoryOptions.");
            userManager.PasswordValidator.Should().BeOfType<MinimumLengthValidator>("because it has be created using the ApplicationUserManager with correct IdentityFactoryOptions.");
            userManager.UserTokenProvider.Should().BeNull("because it has be created using the ApplicationUserManager with correct IdentityFactoryOptions.");
        }
    }
}