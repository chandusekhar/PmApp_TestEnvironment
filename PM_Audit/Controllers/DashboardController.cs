using PM_Audit.Models.Authorization_Models;
using PM_Audit.Models.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PM_Audit.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {
            var ID = User.Identity.GetClaimValue(CustomClaims.Sid).Value;
            var Name = User.Identity.GetClaimValue(CustomClaims.FriendlyName).Value;
            var IsJazz = User.Identity.GetClaimValue(CustomClaims.IsJazz).Value;
            ViewBag.IsJazz = IsJazz;
            ViewBag.Permissions = User.Identity.GetPermissionsList();
            //var enc = BusinessLayer.Utilities.EncryptionUtility.Encrypt("Test");
            //var dec = BusinessLayer.Utilities.EncryptionUtility.Decrypt(enc);


            if (TempData["username"] != null && TempData["password"] != null)
            {
                ViewBag.username = BusinessLayer.Utilities.EncryptionUtility.Encrypt(TempData["username"] == null ? "" : TempData["username"].ToString());
                ViewBag.password = BusinessLayer.Utilities.EncryptionUtility.Encrypt(TempData["password"] == null ? "" : TempData["password"].ToString());
            }
            return View();
        }

        [ChildActionOnly]
        public ActionResult PartialHeader()
        {
            var model = new UserInfoVM();
            model.Email = User.Identity.GetClaimValue(CustomClaims.Email).Value;
            model.UserName = User.Identity.GetClaimValue(CustomClaims.UserName).Value;
            model.FriendlyName = User.Identity.GetClaimValue(CustomClaims.FriendlyName).Value;
            return PartialView("PartialHeader", model);
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}