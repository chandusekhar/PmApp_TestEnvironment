using PM_Audit.Models.Authorization_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PM_Audit.Controllers
{
   [Authorize]
    public class AlarmManagementController : Controller
    {
      [Authorize] // GET: AlarmManagement
        public ActionResult Index()
        {
            if (!User.Identity.HasPermission("perm_alarm_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }
    }
}