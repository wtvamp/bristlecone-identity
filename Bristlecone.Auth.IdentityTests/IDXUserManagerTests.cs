using FluentAssertions;
using NUnit.Framework;
using Bristlecone.Auth.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Moq;

namespace Bristlecone.Auth.IdentityTests
{
    [TestFixture]
    public class BristleconeUserManagerTests
    {
        private Mock<BristleconeAuthDbContext> _authContext;

        [SetUp]
        public void Init()
        {
            var authContextHelper = new MockAuthContext();
            _authContext = authContextHelper.GetMockAuthContext();
        }

        [Test]
        public void Given_an_BristleconeUserManager_when_a_user_is_created_then_identity_properties_like_UserValidator_are_available()
        {
            // GIVEN a mocked IOwinContext and new BristleconeUserStore/BristleconeUserManager
            var _mockAppBuilder = new Mock<IOwinContext>();
            _mockAppBuilder.Setup(f => f.Get<BristleconeAuthDbContext>(It.IsAny<string>())).Returns(_authContext.Object);

            var store = new BristleconeUserStore(_authContext.Object);
            var userManager = new BristleconeUserManager(store);

            var identifyFactoryOptions = new IdentityFactoryOptions<BristleconeUserManager>
            {
                DataProtectionProvider = new DpapiDataProtectionProvider()
            };

            // WHEN a user is created using the BristleconeUserManager
            BristleconeUserManager.Create(identifyFactoryOptions, _mockAppBuilder.Object);

            // THEN the correct user is created with a UserTokerProvider, UserValidator, and PasswordValidator
            userManager.UserValidator.Should().BeOfType<UserValidator<BristleconeUser, string>>("because it has be created using the BristleconeUserManager with correct IdentityFactoryOptions.");
            userManager.PasswordValidator.Should().BeOfType<MinimumLengthValidator>("because it has be created using the BristleconeUserManager with correct IdentityFactoryOptions.");
            userManager.UserTokenProvider.Should().BeNull("because it has be created using the BristleconeUserManager with correct IdentityFactoryOptions.");
        }
    }
}