using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PM_Audit.Models.Authorization_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(PM_Audit.App_Start.Startup))]
namespace PM_Audit.App_Start
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Login/"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity =
                        SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                            validateInterval: TimeSpan.FromMinutes(120),
                            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)),
                    
                           

                },
            });
         //   app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

          //  app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //// Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            //// Enables the application to remember the second login verification factor such as phone or email.
            //// Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            //// This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            //// Uncomment the following lines to enable logging in with third party login providers
            ////app.UseMicrosoftAccountAuthentication(
            ////    clientId: "",
            ////    clientSecret: "");

            ////app.UseTwitterAuthentication(
            ////   consumerKey: "",
            ////   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "1709389546054439",
            //   appSecret: "4776ff97a35ef9de68d25505fb7d1c99");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "329219046537-ejlbcc04lm46j0oi3h21h3uch691fg9t.apps.googleusercontent.com",
            //    ClientSecret = "bPe0_jl9ooRZuEFBAFIp1jSp"
            //});
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(120),
                Provider = new OAuthAutherizationProvider()
            };

            //for mobile token

            //OAuthAuthorizationServerOptions OAuthMobileServerOptions = new OAuthAuthorizationServerOptions()
            //{
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/Mobiletoken"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
            //    Provider = new OAuthMobileAccessProvider()
            //};


            // Token Generation
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
          //  app.UseOAuthAuthorizationServer(OAuthMobileServerOptions);

        }

    }
}