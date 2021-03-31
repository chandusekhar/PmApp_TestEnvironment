using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PM_Audit.Models.Authorization_Models
{
    public class OAuthAutherizationProvider : OAuthAuthorizationServerProvider
    {
        private string sDomain = "Mobilink.net.pk"; //"10.50.12.11:389 & 10.50.12.12:389"; Mobilink.net.pk lhe-dc2k8-01.mobilink.net.pk
        private string sDefaultOU = "ou=users,ou=system";
        private string sServiceUser = @"Mobilink\jazznoc";
        private string sServicePassword = "Noc@7861";
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var isValidUser = false;
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                var appManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                using (PrincipalContext pc = GetPrincipalContext())
                {
                    if (pc.ValidateCredentials(context.UserName, context.Password))             // If User is JAzz user and Is  Present In Active Directory
                    {
                        var user = await appManager.FindByNameAsync(context.UserName);
                        if (user != null)
                        {
                            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                            identity.AddClaim(new Claim(CustomClaims.Sid, user.Id.ToString()));
                            identity.AddClaim(new Claim(CustomClaims.UserName, user.UserName));
                            identity.AddClaim(new Claim(CustomClaims.Email, user.PrimaryEmail));
                            identity.AddClaim(new Claim(CustomClaims.IsJazz, "true"));
                            var props = new AuthenticationProperties(new Dictionary<string, string>
                                     {
                                         {
                                             "client_id", user.Id.ToString()
                                         },
                                         {
                                             "userName", user.UserName
                                         }
                                     });
                            var ticket = new AuthenticationTicket(identity, props);
                            context.Validated(ticket);
                            isValidUser = true;
                        }
                    }
                    else                                                                            // If User is NON JAZZ and Is a Third Party Vendor
                    {
                        var user = await appManager.FindAsync(context.UserName, context.Password);
                        if (user != null)
                        {
                            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                            identity.AddClaim(new Claim(CustomClaims.Sid, user.Id.ToString()));
                            identity.AddClaim(new Claim(CustomClaims.UserName, user.UserName));
                            identity.AddClaim(new Claim(CustomClaims.Email, user.PrimaryEmail));
                            identity.AddClaim(new Claim(CustomClaims.IsJazz, "false"));
                            var props = new AuthenticationProperties(new Dictionary<string, string>
                                     {
                                         {
                                             "client_id", user.Id.ToString()
                                         },
                                         {
                                             "userName", user.UserName
                                         }
                                     });
                            var ticket = new AuthenticationTicket(identity, props);
                            context.Validated(ticket);
                            isValidUser = true;
                        }
                    }
                    if (!isValidUser)
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Domain, sDomain, sDefaultOU,
                                                    ContextOptions.SimpleBind, sServiceUser, sServicePassword);
            return oPrincipalContext;
        }
    }
}