using NUnit.Framework;
using Bristlecone.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bristlecone.AuthTests
{
    [TestFixture()]
    public class FilterConfigTests
    {
        [Test()]
        public void RegisterGlobalFiltersTest()
        {
            var filterCollection = new GlobalFilterCollection();
            FilterConfig.RegisterGlobalFilters(filterCollection);
            Assert.IsTrue(filterCollection.Count > 0);
            Assert.IsInstanceOf<Filter>(filterCollection.FirstOrDefault());
        }
    }
}