using PM_Audit.Models.Authorization_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PM_Audit.Controllers
{
    [Authorize]
    public class ScheduleManagementController : Controller
    {
        // GET: ScheduleManagement

        [Authorize]
        public ActionResult Index()
        {
            if (!User.Identity.HasPermission("perm_monthly_schedule_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult ViewSchedule()
        {
            if (!User.Identity.HasPermission("perm_monthly_schedule_main"))
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