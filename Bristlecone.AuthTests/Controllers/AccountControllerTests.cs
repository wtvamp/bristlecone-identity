using NUnit.Framework;
using Bristlecone.Auth.Controllers;

namespace Bristlecone.Auth.ControllersTests
{
    [TestFixture()]
    public class AccountControllerTests
    {
        [Test()]
        public void AccountControllerGetUserInfoTest()
        {
            var controller = new AccountController();
            var user = controller.GetUserInfo();
            Assert.IsNotNull(user);
            Assert.IsNotNull(user.Email);
            Assert.IsNotNull(user.HasRegistered);
            Assert.IsNull(user.LoginProvider);
        }

       
    }
}