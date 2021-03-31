using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PM_Audit.Models.Authorization_Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PM_Audit.Controllers
{
    public class LoginController : Controller
    {
        private ApplicationUserManager _userManager;

        // GET: Login
        public ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                TempData.Remove("username");
                TempData.Remove("password");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                var phone = model.Phone;
                ApplicationUser user;
                user = await UserManager.FindAsync(model.Email, model.Password);
                if (user.UserName != null)
                {
                    try
                    {
                        TempData["username"] = model.Email;
                        TempData["password"] = model.Password;
                        await SignInAsync(user, model.RememberMe);
                        return RedirectToAction("Index", "Dashboard");
                    }
                    catch (Exception ex)
                    {
                        return Content(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("LogOnError", "The user name or password provided is incorrect.");
                    model.Email = "";
                    model.Phone = "";
                    model.Password = "";
                    return View(model);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            try
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                var identity = await user.GenerateUserIdentityAsync(UserManager);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut(
                DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Login");
        }
    }
}