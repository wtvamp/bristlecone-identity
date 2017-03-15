using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bristlecone.Auth.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;

using Owin;
using Bristlecone.Auth.Providers;
using Bristlecone.Auth.Models;
using Microsoft.Owin.Security.Facebook;
using Owin.Security.Providers.LinkedIn;

namespace Bristlecone.Auth
{
    public partial class Startup
    {
        
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        [ExcludeFromCodeCoverage]
        private static void setUserType(IAppBuilder app, IOAuthAuthorizationServerProvider provider)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(BristleconeAuthDbContext.Create);
            app.CreatePerOwinContext<BristleconeUserManager>(BristleconeUserManager.Create);
        }

        [ExcludeFromCodeCoverage]
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth<T>(IAppBuilder app) where T : class
        {
            IOAuthAuthorizationServerProvider provider = null;
            setUserType(app, provider);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = Activator.CreateInstance(typeof(T),new object[] { PublicClientId }) as OAuthAuthorizationServerProvider,
                AuthorizeEndpointPath = new PathString($"/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //var facebookProvider = new FacebookAuthenticationProvider()
            //{
            //    OnAuthenticated = (context) =>
            //    {
            //        // Add the email id to the claim
            //        context.Identity.AddClaim(new Claim(ClaimTypes.Email, context.Email));
            //        return Task.FromResult(0);
            //    }
            //};
            //var options = new FacebookAuthenticationOptions()
            //{
            //    AppId = "1768972796678680",
            //    AppSecret = "d7053829d8a24e4c02b68ac9613f73a9",
            //    Provider = facebookProvider
            //};

            //options.Scope.Add("email");
            //app.UseFacebookAuthentication(options);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "77896847526-ot1ltoknk6u1puhvmrlprfdfun60gucn.apps.googleusercontent.com",
                ClientSecret = "QRjLw3PT-ehygH3Oog7qdkNJ"
            });

            app.UseLinkedInAuthentication(new LinkedInAuthenticationOptions
            {
                ClientId = "77rkpk9ecvpk55",
                ClientSecret = "RFDQGJAvhWMScjq1",
            });
        }
    }
}
