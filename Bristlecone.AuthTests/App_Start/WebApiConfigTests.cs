using NUnit.Framework;
using Bristlecone.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bristlecone.AuthTests
{
    [TestFixture()]
    public class WebApiConfigTests
    {
        [Test()]
        public void RegisterTest()
        {
            var httpConfig = new HttpConfiguration();
            WebApiConfig.Register(httpConfig);
            Assert.IsTrue(httpConfig.Filters.Count > 0);
            Assert.IsTrue(httpConfig.Routes.Count > 0);
        }
    }
}