using BusinessLayer.ContextClasses;
using BusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Managers
{
    public static class AuthenticationManager
    {
        public static UserVM GetUsersByUserName(string UserName)
        {
            try
            {
                using (var context = new PMDashbordDBContext())
                {
                    //var MLAdUser = context.Database.SqlQuery<UserVM>("WEB_PROC_PM_GET_AD_USER_DETAIL @UserName", new SqlParameter("@UserName", UserName)).FirstOrDefault();
                    //if (MLAdUser != null)
                    //    MLAdUser.IsJazz = true;
                    var MLAdUser = context.Users.Where(x => x.UserName == UserName).Select(y => new UserVM()
                    {
                        FriendlyName = y.FriendlyName,
                        Id = y.Id,
                        Password = y.Password,
                        IsJazz = false,
                        PasswordEnc = y.PasswordEnc,
                        PrimaryEmail = y.PrimaryEmail,
                        Telephone = y.Telephone,
                        UpdateTime = y.UpdateTime,
                        UserName = y.UserName,
                    }).FirstOrDefault();
                    return MLAdUser;
                }
            }
            catch (Exception Ex)
            {

                throw;
            }

        }

        public static UserVM AuthenticateVendorUser(string UserName, string password)
        {
            try
            {
                using (var context = new PMDashbordDBContext())
                {
                    //var vendorUser = context.Database.SqlQuery<UserVM>("WEB_PROC_PM_GET_VENDOR_DETAIL @UserName,@Password", new SqlParameter("@UserName", UserName), new SqlParameter("@Password", password)).FirstOrDefault();
                    //if (vendorUser != null)
                    //    vendorUser = false;
                    var vendorUser = context.Users.Where(x => x.UserName == UserName && x.Password == password).Select(y => new UserVM()
                    {
                        FriendlyName = y.FriendlyName,
                        Id = y.Id,
                        Password = y.Password,
                        IsJazz = false,
                        PasswordEnc = y.PasswordEnc,
                        PrimaryEmail = y.PrimaryEmail,
                        Telephone = y.Telephone,
                        UpdateTime = y.UpdateTime,
                        UserName = y.UserName,
                    }).FirstOrDefault();

                    return vendorUser;

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public static List<string> GetUsersPermissions(string UserName)
        {
            try
            {
                using (var context = new PMDashbordDBContext())
                {
                    var userPermissions = context.Database.SqlQuery<string>("WEB_PROC_PM_GET_USER_PERMISSIONS_NAMES @UserName", new SqlParameter("@UserName", UserName)).ToList();
                    return userPermissions;

                }
            }
            catch (Exception Ex)
            {

                throw;
            }

        }

        public static bool IsPrincipalContextValidated(string UserName, string Password)
        {
            try
            {
                using (PrincipalContext pc = GetPrincipalContext())
                {
                    return pc.ValidateCredentials(UserName, Password);             // If User is JAzz user and Is  Present In Active Directory
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            
        }
        private static PrincipalContext GetPrincipalContext()
        {
            string sDomain = "Mobilink.net.pk"; //"10.50.12.11:389 & 10.50.12.12:389"; Mobilink.net.pk lhe-dc2k8-01.mobilink.net.pk
            string sDefaultOU = "ou=users,ou=system";
            string sServiceUser = @"Mobilink\jazznoc";
            string sServicePassword = "Noc@7861";
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Domain, sDomain, sDefaultOU,
                                                    ContextOptions.SimpleBind, sServiceUser, sServicePassword);
            return oPrincipalContext;
        }
    }
}
