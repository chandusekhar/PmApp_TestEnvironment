using PM_Audit.Models.Authorization_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PM_Audit.Controllers
{
    public class AuditController : Controller
    {
         [Authorize]
        // GET: Audit
        public ActionResult Index()
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult ViewPmDetailsListForAudit(string Site_Code, string PM_Type)
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_Type))
            {
                return RedirectToAction("Index", "Audit");
            }
            ViewBag.Site_Code = Site_Code;
            ViewBag.PM_Type = PM_Type;
           // return View("~/Views/Audit/ViewPMDetailsListforAudit.cshtml");
           return View();
        }

        [Authorize]
        public ActionResult ViewAuditListForComparisonReport(string Site_Code, string PM_Type)
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_Type))
            {
                return RedirectToAction("Index", "Audit");
            }
            ViewBag.Site_Code = Site_Code;
            ViewBag.PM_Type = PM_Type;
            //return View("~/Views/Audit/ViewAuditListForComparisonReport.cshtml");
            return View();
        }



        [Authorize]
        public ActionResult ViewTelcoPMDetailsforAudit(string Site_Code, string PM_ID)
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_ID))
            {
                return RedirectToAction("Index", "Audit");
            }
            ViewBag.PM_ID = PM_ID;
            ViewBag.Site_Code = Site_Code;
            return View("~/Views/Audit/ViewTelcoPMDetailsforAudit.cshtml");
        }
        [Authorize]
        public ActionResult ViewNoNTelcoPMDetailsforAudit(string Site_Code, string PM_ID)
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_ID))
            {
                return RedirectToAction("Index", "Audit");
            }
            ViewBag.PM_ID = PM_ID;
            ViewBag.Site_Code = Site_Code;
            return View("~/Views/Audit/ViewNonTelcoPMDetailsforAudit.cshtml");
        }

        [Authorize]
        public ActionResult Audit_Reports()
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult ViewAuditDetailsReport()
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult ViewGeneralAuditDetailsReport()
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }


        [Authorize]
        public ActionResult ViewComparison(string Site_Code, string PM_Type, string PM_ID)
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Site_Code = Site_Code;
                ViewBag.PM_Type = PM_Type;
                ViewBag.PM_ID = PM_ID;

                if (PM_Type == "Telco")
                {
                    return View("~/Views/Audit/TelcoComparison_Report.cshtml");
                }
                else
                {
                    return View("~/Views/Audit/NonTelcoComparison_Report.cshtml");
                }
            }
        }

        [Authorize]
        public ActionResult ViewAuditDetails(string Site_Code, string PM_Type,string FK_PM_ID, string PM_ID)
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Site_Code = Site_Code;
                ViewBag.PM_ID = PM_ID;
                ViewBag.FK_PM_ID = FK_PM_ID;
                ViewBag.PM_Type = PM_Type;
                if (PM_Type == "Telco")
                {
                    return View("~/Views/Audit/ViewTelcoAuditDetails.cshtml");
                }
                else if (PM_Type == "NON-Telco")
                {
                    return View("~/Views/Audit/ViewNoNTelcoAuditDetails.cshtml");
                }
                else
                {
                    return View("~/Views/Audit/ViewAuditDetailsReport.cshtml");
                }
            }
        }

        [Authorize]
        public ActionResult ViewGeneralAuditDetails(string Site_Code, string PM_Type, string PM_ID)
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Site_Code = Site_Code;
                ViewBag.PM_ID = PM_ID;
                ViewBag.PM_Type = PM_Type;
               
                if (PM_Type == "Telco")
                {
                    return View("~/Views/Audit/GeneralAudit/ViewTelcoGeneralAuditDetails.cshtml");
                }
                else if (PM_Type == "NON-Telco")
                { 
                    return View("~/Views/Audit/GeneralAudit/ViewNoNTelcoGeneralAuditDetails.cshtml");
                }

                else
                {
                    return View("~/Views/Audit/ViewGeneralAuditDetailsReport.cshtml");
                }

            }
        }

        [Authorize]
        public ActionResult GeneralAuditTelco(string Site_Code, string Audit_ID)
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(Audit_ID))
            {
                return RedirectToAction("Index", "Audit");
            }
            ViewBag.General_Audit_Telco_ID = Audit_ID;
            ViewBag.General_Audit_Telco_Site_Code = Site_Code;
            return View("~/Views/Audit/GeneralAudit/GeneralAuditTelco.cshtml");
        }
        [Authorize]
        public ActionResult GeneralAuditNonTelco(string Site_Code, string Audit_ID)
        {
            if (!User.Identity.HasPermission("perm_audit_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(Audit_ID))
            {
                return RedirectToAction("Index", "Audit");
            }
            ViewBag.General_Audit_Non_Telco_ID = Audit_ID;
            ViewBag.General_Audit_Non_Telco_Site_Code = Site_Code;
            return View("~/Views/Audit/GeneralAudit/GeneralAuditNonTelco.cshtml");
        }
    }
}