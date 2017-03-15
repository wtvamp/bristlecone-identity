using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Bristlecone.Auth.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("BristleconeAuth", typeof(Bristlecone.Auth.Startup))]

namespace Bristlecone.Auth
{
    [ExcludeFromCodeCoverage]
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ConfigureAuth<Bristlecone.Auth.Providers.BristleconeOAuthProvider>(app);
        }
    }
}
