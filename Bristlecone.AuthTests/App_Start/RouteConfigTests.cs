using NUnit.Framework;
using Bristlecone.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bristlecone.AuthTests
{
    [TestFixture()]
    public class RouteConfigTests
    {
        [Test()]
        public void RegisterRoutesTest()
        {
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            Assert.IsTrue(routeCollection.Count > 0);
            Assert.IsInstanceOf<Route>(routeCollection.FirstOrDefault());
        }
    }
}