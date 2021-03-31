using BusinessLayer.ContextClasses;
using BusinessLayer.Managers;
using BusinessLayer.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PM_Audit.Models.Authorization_Models
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager()
            : base(new ApplicationUserStore<ApplicationUser>())
        {
            //We can retrieve Old System Hash Password and can encypt or decrypt old password using custom approach. 
            //When we want to reuse old system password as it would be difficult for all users to initiate pwd change as per Idnetity Core hashing. 
            this.PasswordHasher = new OldSystemPasswordHasher();
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager();
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                //RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.

            //manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            //{
            //    MessageFormat = "Your security code is: {0}"
            //});
            //manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            //{
            //    Subject = "SecurityCode",
            //    BodyFormat = "Your security code is {0}"
            //});
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public override Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var result = AuthenticationManager.GetUsersByUserName(userName);
            if (result == null)
            {
                result = new BusinessLayer.ViewModels.UserVM();
                result.Permissions = new List<string>();
            }
            else
            {
                result.Permissions = AuthenticationManager.GetUsersPermissions(userName);
            }

            Task<ApplicationUser> user = Task<ApplicationUser>.Factory.StartNew(() =>
            {
                return new ApplicationUser()
                {
                    Id = result.UserName,
                    FriendlyName = result.FriendlyName,
                    PrimaryEmail = result.PrimaryEmail,
                    Telephone = result.Telephone,
                    UserName = result.UserName,
                    Permissions = result.Permissions
                };
            });
            return user;
        }

        public override async Task<ApplicationUser> FindAsync(string userName, string password)
        {
            try
            {
                var ApiPath = ConfigurationManager.AppSettings["ApiPath"].ToString()+"api/";

                UserVM userWithPermissions = new UserVM() ;
                var UserModel = new AuthenticationVM() { UserName = userName, Password = password };

                using (var client = new HttpClient())
                {

                    //   client.BaseAddress = new Uri("http://localhost:14529/api/");
                       client.BaseAddress = new Uri(ApiPath);

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //    var uri = "http://localhost:14529/api/Accounts/GetUserInfo";
                       var uri = ApiPath+ "Accounts/GetUserInfo";

                    var response = await client.PostAsJsonAsync(uri, UserModel);
                    if (response.IsSuccessStatusCode)
                    {
                        userWithPermissions = await response.Content.ReadAsAsync<UserVM>();
                    }
                }
                if (userWithPermissions == null)
                {
                    userWithPermissions = new UserVM();
                }
                Task<ApplicationUser> user = Task<ApplicationUser>.Factory.StartNew(() =>
                    {
                        return new ApplicationUser()
                        {
                            Id = userWithPermissions.UserName,
                            FriendlyName = userWithPermissions.FriendlyName,
                            PrimaryEmail = userWithPermissions.PrimaryEmail,
                            Telephone = userWithPermissions.Telephone,
                            UserName = userWithPermissions.UserName,
                            IsJazz = userWithPermissions.IsJazz,
                            Permissions = userWithPermissions.Permissions
                        };
                    });
                return await user;
            }
            catch (Exception EX)
            {

                throw;
            }
        }

        public override Task<ApplicationUser> FindByIdAsync(string ID)
        {
            var result = AuthenticationManager.GetUsersByUserName(ID);
            if (result == null)
            {
                result = new BusinessLayer.ViewModels.UserVM();
                result.Permissions = new List<string>();
            }
            else
            {
                result.Permissions = AuthenticationManager.GetUsersPermissions(ID);
            }

            Task<ApplicationUser> user = Task<ApplicationUser>.Factory.StartNew(() =>
            {
                return new ApplicationUser()
                {
                    Id = result.UserName,
                    FriendlyName = result.FriendlyName,
                    PrimaryEmail = result.PrimaryEmail,
                    Telephone = result.Telephone,
                    UserName = result.UserName,
                    Permissions = result.Permissions
                };
            });
            return user;
        }
    }
    public class OldSystemPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {

            //Here we will place the code of password hashing that is there in our current solucion.This will take cleartext anad hash
            //Just for demonstration purpose I always return true.	
            if (true)
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
                //}
                //else
                //{
                //    return PasswordVerificationResult.Failed;
                //}
            }
        }
    }
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
