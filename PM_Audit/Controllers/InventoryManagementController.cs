using PM_Audit.Models.Authorization_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PM_Audit.Controllers
{
    [Authorize]
    public class InventoryManagementController : Controller
    {

        [Authorize]
        // GET: InventoryManagement
        public ActionResult Index()
        {
            if (!User.Identity.HasPermission("perm_inventory_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        public ActionResult UnApprovedInventoryList()
        {
            if (!User.Identity.HasPermission("perm_inventory_management_approve_inventory"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult ViewInventory(string Site_Code, string PM_ID = "", string PM_Type = "", string Close_Type = "")
        {
            if (!User.Identity.HasPermission("perm_inventory_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.SiteCode = Site_Code;
                ViewBag.PM_ID = string.IsNullOrWhiteSpace(PM_ID) ? "0" : PM_ID;
                ViewBag.PM_Type = string.IsNullOrWhiteSpace(PM_Type) ? "" : PM_Type;
                ViewBag.Close_Type = string.IsNullOrWhiteSpace(Close_Type) ? "success" : Close_Type;

                return View();
            }
        }

        [Authorize]
        public ActionResult ViewTelcoInventory(string Site_Code, string PM_ID = "", string PM_Type = "", string Close_Type = "")
        {
            if (!User.Identity.HasPermission("perm_inventory_management_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.SiteCode = Site_Code;
                ViewBag.PM_ID = string.IsNullOrWhiteSpace(PM_ID) ? "0" : PM_ID;
                ViewBag.PM_Type = string.IsNullOrWhiteSpace(PM_Type) ? "" : PM_Type;
                ViewBag.Close_Type = string.IsNullOrWhiteSpace(Close_Type) ? "success" : Close_Type;

                return View();
            }
        }


        [Authorize]
        public ActionResult ApproveInventory(string Site_Code, string PM_ID = "", string PM_Type = "", string Close_Type = "", string Inventory_ID = "")
        {
            if (!User.Identity.HasPermission("perm_inventory_management_approve_inventory"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.SiteCode = Site_Code;
                ViewBag.PM_ID = string.IsNullOrWhiteSpace(PM_ID) ? "0" : PM_ID;
                ViewBag.Inventory_ID = string.IsNullOrWhiteSpace(Inventory_ID) ? "" : Inventory_ID;
                ViewBag.PM_Type = string.IsNullOrWhiteSpace(PM_Type) ? "" : PM_Type;
                ViewBag.Close_Status = string.IsNullOrWhiteSpace(Close_Type) ? "success" : Close_Type;

                return View("~/Views/InventoryManagement/InventoryApproval/ViewInventoryForApproval.cshtml");
            }
        }

        [Authorize]
        public ActionResult ApproveTelcoInventory(string Site_Code, string PM_ID = "", string PM_Type = "", string Close_Type = "", string Inventory_ID = "")
        {
            if (!User.Identity.HasPermission("perm_inventory_management_approve_inventory"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.SiteCode = Site_Code;
                ViewBag.PM_ID = string.IsNullOrWhiteSpace(PM_ID) ? "0" : PM_ID;
                ViewBag.Inventory_ID = string.IsNullOrWhiteSpace(Inventory_ID) ? "" : Inventory_ID;
                ViewBag.Close_Status = string.IsNullOrWhiteSpace(Close_Type) ? "success" : Close_Type;
                ViewBag.PM_Type = string.IsNullOrWhiteSpace(PM_Type) ? "" : PM_Type;

                return View("~/Views/InventoryManagement/InventoryApproval/ViewTelcoInventoryForApproval.cshtml");
            }
        }
    }
}