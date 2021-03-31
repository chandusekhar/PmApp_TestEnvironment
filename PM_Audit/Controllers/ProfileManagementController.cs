using PM_Audit.Models.Authorization_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PM_Audit.Controllers
{
    public class ProfileManagementController : Controller
    {
        // GET: ChangePassword
		 [Authorize]
        public ActionResult Index()
        {
                return View();
        }
    }
}