using BusinessLayer.Managers;
using BusinessLayer.ViewModels;
using Newtonsoft.Json;
using PM_Audit.Models.Authorization_Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PM_Audit.Controllers
{
    [Authorize]
    public class PrimeTPrimeController : Controller
    {
        // GET: PrimeTPrime
        #region Prime_TPrime_Ticketing
        public ActionResult Index()
        {
            if (User.Identity.HasPermission("perm_prime_tprime_open") || User.Identity.HasPermission("perm_prime_tprime_view"))
            {

                ViewBag.PrimeTPrimePermissions = User.Identity.GetPermissionsList();
                var checkCookieExistance = PreventiveMaintenanceManager.getCookie("LtpaToken2", "LtpaToken2");
                if (checkCookieExistance == "")
                {
                    var cookiesList = PreventiveMaintenanceManager.AuthenticateMaximo();
                    if (cookiesList != null)
                    {
                        var cookieObj = PreventiveMaintenanceManager.setCookie(cookiesList[0].Name, cookiesList[0].Value, 30);
                        Response.Cookies.Add(cookieObj);
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }
        public ActionResult OpenTicket()
        {
            if (!User.Identity.HasPermission("perm_prime_tprime_open"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        public ActionResult ViewTicket()
        {
            if (!User.Identity.HasPermission("perm_prime_tprime_view"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        public ActionResult ViewTicketForApproval(int id)
        {
            ViewBag.primeId = id;
            return View();
        }
        public ActionResult ViewTicketForResolution(int id)
        {
            ViewBag.primeId = id;
            return View();
        }
        public ActionResult ViewTicketForCloseReopen(int id)
        {
            ViewBag.primeId = id;
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
        public JsonResult GetAllTickets()
        {
            var cookie = PreventiveMaintenanceManager.getCookie("LtpaToken2", "LtpaToken2");
            checkCookieExistence(cookie);
            var currentUser = User.Identity.GetClaimValue(CustomClaims.UserName).Value;
            var data = PreventiveMaintenanceManager.getAllTickets(currentUser);
            if (!data.Contains("error"))
            {
                var obj = JsonConvert.DeserializeObject<List<GetTicketsVM>>(data);
                List<GetTicketsVM> finalObj = new List<GetTicketsVM>();
                foreach (var item in obj)
                {
                    if (item.createdate != "")
                    {
                        var dateTime = Convert.ToDateTime(item.createdate).AddHours(5);
                        item.createdate = dateTime.ToString("g", CultureInfo.CreateSpecificCulture("en-us"));
                        finalObj.Add(item);
                    }
                }
                var result = new JsonResult
                {
                    Data = finalObj
                };
                //var dateTime = Convert.ToDateTime(result.Data.cr)
                return Json(new { Data = finalObj, success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = data, success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveTicket(SaveTicketsVM model)
        {
            model.sitestatus = "D-Prime";
            var checkCookie = PreventiveMaintenanceManager.getCookie("LtpaToken2", "LtpaToken2");
            checkCookieExistence(checkCookie);
            model.createdby = User.Identity.GetClaimValue(CustomClaims.UserName).Value;
            //model.createdate = DateTime.Now;
            var response = PreventiveMaintenanceManager.saveTicket(model);
            return Json(new { response = response });
        }
        public JsonResult ViewTicketDetails(int id)
        {
            var checkCookie = PreventiveMaintenanceManager.getCookie("LtpaToken2", "LtpaToken2");
            checkCookieExistence(checkCookie);
            var response = PreventiveMaintenanceManager.ViewTicketDetails(id);
            if (!response.Contains("error"))
            {
                var result = new JsonResult
                {
                    Data = JsonConvert.DeserializeObject<GetTicketsVM>(response)
                };
                return Json(new { data = result, success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = response, success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateTicket(int id, SaveTicketsVM model, string viewName)
        {
            var checkCookie = PreventiveMaintenanceManager.getCookie("LtpaToken2", "LtpaToken2");
            checkCookieExistence(checkCookie);
            model.changeby = User.Identity.GetClaimValue(CustomClaims.UserName).Value;
            model.changedate = DateTime.UtcNow.ToString();
            var response = PreventiveMaintenanceManager.UpdateTicket(id, model, viewName);
            return Json(response);
        }
        //[HttpPost]
        public JsonResult GetReportData(GetTicketsVM model)
        {
            var checkCookie = PreventiveMaintenanceManager.getCookie("LtpaToken2", "LtpaToken2");
            checkCookieExistence(checkCookie);
            var response = PreventiveMaintenanceManager.getReportData(model);
            if (!response.Contains("error"))
            {
                var obj = JsonConvert.DeserializeObject<List<GetTicketsVM>>(response);
                var result = new JsonResult
                {
                    Data = obj
                };
                return Json(new { success = true, data = obj });
            }
            else
            {
                return Json(new { success = false, data = response });
            }
        }
        private void checkCookieExistence(string cookie)
        {
            if (cookie == "")
            {
                var cookiesList = PreventiveMaintenanceManager.AuthenticateMaximo();
                if (cookiesList != null)
                {
                    var cookieObj = PreventiveMaintenanceManager.setCookie(cookiesList[0].Name, cookiesList[0].Value, 30);
                    Response.Cookies.Add(cookieObj);
                }
            }
        }
        #endregion
    }
}