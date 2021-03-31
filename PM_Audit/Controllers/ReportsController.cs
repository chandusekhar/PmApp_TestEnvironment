using PM_Audit.Models.Authorization_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMAdminDashboard.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        [Authorize]
        public ActionResult Index()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult ExtractReports()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult ChecklistReport()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult InventoryReport()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult NonPMReport()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult ViewTelcoPMDetails(string Site_Code, string PM_Type, string PM_ID)
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_Type) || string.IsNullOrWhiteSpace(PM_ID))
            {
                return RedirectToAction("ChecklistReport", "Reports");
            }
            ViewBag.PM_ID = PM_ID;
            ViewBag.Site_Code = Site_Code;
            return View("~/Views/Reports/PMDetails/ViewTelcoPm.cshtml");
        }
        [Authorize]
        public ActionResult ViewNoNTelcoPMDetails(string Site_Code, string PM_Type, string PM_ID)
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_Type) || string.IsNullOrWhiteSpace(PM_ID))
            {
                return RedirectToAction("ChecklistReport", "Reports");
            }
            ViewBag.PM_ID = PM_ID;
            ViewBag.Site_Code = Site_Code;

            return View("~/Views/Reports/PMDetails/ViewNonTelcoPm.cshtml");
        }

        [Authorize]
        public ActionResult InventoryDetails(string Site_Code)
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.SiteCode = Site_Code;
                return View();
            }
        }


        [Authorize]
        public ActionResult SpecifficInventoryIDDetails(string Site_Code,string Inventory_ID, string Inventory_Type)
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.SiteCode = Site_Code;
                ViewBag.Inventory_ID = Inventory_ID;
                if (Inventory_Type=="NON-Telco")
                    return View("~/Views/Reports/ViewSpecifficInventoryDetails.cshtml");
                else
                return View("~/Views/Reports/ViewSpecifficTelcoInventoryDetails.cshtml");
            }
        }



        [Authorize]
        public ActionResult TelcoInventoryDetails(string Site_Code)
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.SiteCode = Site_Code;
                return View();
            }
        }


        [Authorize]
        public ActionResult DrillDownReport()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult PMPendingApproval()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult InventoryPendingApproval()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult AlarmTogglingReport()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        public ActionResult CpDetails()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DgDetails()
        {
            if (!User.Identity.HasPermission("perm_reports_main"))
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