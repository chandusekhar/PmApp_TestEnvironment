using BusinessLayer.Managers;
using BusinessLayer.ViewModels;
using Newtonsoft.Json;
using PM_Audit.Models;
using PM_Audit.Models.Authorization_Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
namespace PM_Audit.Controllers
{
    //[Authorize]
    public class PreventiveMaintenanceController : Controller
    {
        // GET: PreventiveMaintenance
        [Authorize]
        public ActionResult Index()
        {

            if (!User.Identity.HasPermission("perm_preventive_maintenance_main"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Permissions = User.Identity.GetPermissionsList();
            return View();

            var model = new MyViewModel
            {
                MyBooleanValue = true
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(MyViewModel model)
        {
            if (model.MyBooleanValue)
            {
                string text = model.MyTextValue;
            }

            return View(model);
        }
        [Authorize]
        public ActionResult ViewPM()
        {
            if (!User.Identity.HasPermission("perm_start_pm"))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }
        [Authorize]
        public ActionResult ViewPmForApproval()
        {
            if (!User.Identity.HasPermission("perm_approve_pm"))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View("~/Views/PreventiveMaintenance/PMApproval/ViewPmForApproval.cshtml");
        }
        [Authorize]
        public ActionResult ViewPendingPms()
        {
            if (!User.Identity.HasPermission("perm_pending_pm"))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View("~/Views/PreventiveMaintenance/ViewPendingPms.cshtml");
        }
        [Authorize]
        public ActionResult ViewPmForReOpen()
        {
            if (!User.Identity.HasPermission("perm_start_pm"))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View("~/Views/PreventiveMaintenance/ViewPmForReOpen.cshtml");
        }


        [Authorize]
        [HttpPost]
        public ActionResult ClosePM(string Site_Code, string PM_Type, long PM_ID)
        {
            if (!User.Identity.HasPermission("perm_start_pm"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_Type) || PM_ID == 0)
            {
                return RedirectToAction("ViewPM", "PreventiveMaintenance");
            }
            ViewBag.Site_Code = Site_Code;
            ViewBag.SelectedPM_ID = PM_ID;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ClosePM_NonTelco(string Site_Code, string PM_Type, long PM_ID)
        {
            if (!User.Identity.HasPermission("perm_start_pm"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_Type) || PM_ID == 0)
            {
                return RedirectToAction("ViewPM", "PreventiveMaintenance");
            }
            ViewBag.Site_Code = Site_Code;
            ViewBag.SelectedPM_ID = PM_ID;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ReOpenPM(string Site_Code, string PM_Type, long PM_ID)
        {
            if (!User.Identity.HasPermission("perm_start_pm"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_Type) || PM_ID == 0)
            {
                return RedirectToAction("ViewPM", "PreventiveMaintenance");
            }
            ViewBag.Site_Code = Site_Code;
            ViewBag.SelectedPM_ID = PM_ID;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ReOpenPM_NonTelco(string Site_Code, string PM_Type, long PM_ID)
        {
            if (!User.Identity.HasPermission("perm_start_pm"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_Type) || PM_ID == 0)
            {
                return RedirectToAction("ViewPM", "PreventiveMaintenance");
            }
            ViewBag.Site_Code = Site_Code;
            ViewBag.SelectedPM_ID = PM_ID;
            return View();
        }
        [Authorize]
        public ActionResult ViewTelcoPmForApproval(string Site_Code, string PM_Type, string PM_ID)
        {
            if (!User.Identity.HasPermission("perm_approve_pm"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_Type) || string.IsNullOrWhiteSpace(PM_ID))
            {
                return RedirectToAction("ViewPmForApproval", "PreventiveMaintenance");
            }
            ViewBag.PM_ID = PM_ID;
            ViewBag.Site_Code = Site_Code;

            return View("~/Views/PreventiveMaintenance/PMApproval/ViewTelcoPmForApproval.cshtml");
        }

        [Authorize]
        public ActionResult ViewNonTelcoPmForApproval(string Site_Code, string PM_Type, string PM_ID)
        {
            if (!User.Identity.HasPermission("perm_approve_pm"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            if (string.IsNullOrWhiteSpace(Site_Code) || string.IsNullOrWhiteSpace(PM_Type) || string.IsNullOrWhiteSpace(PM_ID))
            {
                return RedirectToAction("ViewPmForApproval", "PreventiveMaintenance");
            }

            ViewBag.PM_ID = PM_ID;
            ViewBag.Site_Code = Site_Code;
            return View("~/Views/PreventiveMaintenance/PMApproval/ViewNonTelcoPmForApproval.cshtml");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public JsonResult Upload_Image(ImageModel model)
        {
            //if (!User.Identity.HasPermission("perm_start_pm"))
            //{
            //    //return Json("No Permissions");
            //    //return RedirectToAction("Index", "Dashboard");
            //    return Json(new
            //    {
            //        Url.Action("","")

            //    });
            //}
            //else
            // {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            //string[] testfiles = model.id.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;

                        }
                        var extension = fname.Split('.').Last();
                        var Foldername = model.PM_ID + "_" + model.Site_Code + "_" + model.Type;
                        //Check to delete previous folder and create new one
                        string partialName = model.Site_Code + "_" + model.Type;
                        DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(Server.MapPath("~/Content/Uploads/"));
                        if (!Directory.Exists(Foldername))
                        {


                            DirectoryInfo[] dirsInDir = hdDirectoryInWhichToSearch.GetDirectories("*" + partialName);
                            var dirLen = dirsInDir.Length;
                            if (dirLen != 0)
                            {
                                FileInfo[] filesInDir = dirsInDir[0].GetFiles();
                                if (dirsInDir != null)
                                {
                                    if (!(dirsInDir[0].FullName.Contains(model.PM_ID)))
                                    {
                                        foreach (FileInfo item in filesInDir)
                                        {
                                            string fullname = item.FullName;
                                            System.IO.File.Delete(fullname);
                                        }
                                        System.IO.Directory.Delete(Path.Combine(Server.MapPath("~/Content/Uploads/"), dirsInDir[0].FullName));
                                    }
                                }
                            }

                            var Imagename = model.id + "." + extension;
                            fname = Path.Combine(Server.MapPath("~/Content/Temp/"), Imagename);
                            file.SaveAs(fname);
                            Directory.CreateDirectory(Server.MapPath("~/Content/Uploads/" + Foldername));
                            var outpath = Path.Combine(Server.MapPath("~/Content/Uploads/" + Foldername + "/"));
                            var res = CompressImage(fname, 75, outpath);

                            //Delete file from Temp folder
                            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Temp/"), Imagename));
                        }
                        //else
                        //{
                        //    DirectoryInfo[] dirsInDir = hdDirectoryInWhichToSearch.GetDirectories("*" + partialName);
                        //    FileInfo[] filesInDir = dirsInDir[0].GetFiles();
                        //    if (dirsInDir != null)
                        //    {
                        //        if (!(dirsInDir[0].FullName.Contains(model.PM_ID)))
                        //        {
                        //            foreach (FileInfo item in filesInDir)
                        //            {
                        //                string fullname = item.FullName;
                        //                System.IO.File.Delete(fullname);
                        //            }
                        //            System.IO.Directory.Delete(Path.Combine(Server.MapPath("~/Content/Uploads/"), dirsInDir[0].FullName));
                        //        }
                        //    }
                        //    var Imagename = model.id + "." + extension;
                        //    fname = Path.Combine(Server.MapPath("~/Content/Temp/"), Imagename);
                        //    file.SaveAs(fname);
                        //    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads/" + Foldername));
                        //    var outpath = Path.Combine(Server.MapPath("~/Content/Uploads/" + Foldername + "/"));
                        //    var res = CompressImage(fname, 75, outpath);

                        //    //Delete file from Temp folder
                        //    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Temp/"), Imagename));
                        //}


                        //if(System.IO.Directory(hdDirectoryInWhichToSearch)
                        //DirectoryInfo[] dirsInDir = hdDirectoryInWhichToSearch.GetDirectories("*" + partialName);
                        //FileInfo[] filesInDir = dirsInDir[0].GetFiles();                       
                        //if(dirsInDir != null)
                        //{
                        //   if(!(dirsInDir[0].FullName.Contains(model.PM_ID)))
                        //    {
                        //        foreach (FileInfo item in filesInDir)
                        //        {
                        //            string fullname = item.FullName;
                        //            System.IO.File.Delete(fullname);
                        //        }
                        //        System.IO.Directory.Delete(Path.Combine(Server.MapPath("~/Content/Uploads/"), dirsInDir[0].FullName));
                        //    }
                        //}


                        //var Imagename = model.id+"."+ extension;  
                        //    fname = Path.Combine(Server.MapPath("~/Content/Temp/"), Imagename);
                        //    file.SaveAs(fname);
                        //    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads/" + Foldername));
                        //    var outpath = Path.Combine(Server.MapPath("~/Content/Uploads/" + Foldername + "/"));
                        //    var res = CompressImage(fname, 75, outpath);

                        //    //Delete file from Temp folder
                        //    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Temp/"), Imagename));
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                    //  return PartialView("~/Views/PreventiveMaintenance/_Images.cshtml");
                    // return false;
                }
                catch (Exception ex)

                {
                    // return false;
                    return Json("Error occurred. Error details: " + ex.Message);

                }
            }
            else
            {
                //return false;
                return Json("No files selected!");
                //return View("~/Views/PreventiveMaintenance/ClosePM.cshtml");
            }

        }

        public static string CompressImage(string InputImage, int Quality, string OutPutDirectory)
        {
            using (Bitmap mybitmab = new Bitmap(@InputImage))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                System.Drawing.Imaging.Encoder myEncoder =
                       System.Drawing.Imaging.Encoder.Quality;

                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, Quality);
                myEncoderParameters.Param[0] = myEncoderParameter;

                string NewOutputPath = @OutPutDirectory + Path.GetFileNameWithoutExtension(InputImage) + Path.GetExtension(InputImage);

                mybitmab.Save(NewOutputPath, jpgEncoder, myEncoderParameters);

                return NewOutputPath;
            }

        }
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public JsonResult PreviewWordDocument(ImagePathModel model)
        {
            List<ImagePathModel> ObjPath = new List<ImagePathModel>();
            var Foldername = model.PM_ID + "_" + model.Site_Code + "_" + model.Type;
            var dir = Path.Combine(Server.MapPath("~/Content/Uploads/" + Foldername + "/"));
            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    ImagePathModel Obj = new ImagePathModel();
                    FileInfo fileInfo = new FileInfo(file);

                    string path = "/Content/Uploads/" + Foldername + "/" + fileInfo.Name;
                    Obj.Path = path;
                    Obj.Name = fileInfo.Name;

                    ObjPath.Add(Obj);
                }
                List<string> images = null;
                if (model.Type == "Telco")
                {
                    images = new List<string> { "BTS_Image", "Alarms_Image", "Antenna_Image_1", "Antenna_Image_2", "Antenna_Image_3" };
                }
                else
                {
                    images = new List<string> { "Meter_Image", "Sitecondition_Image", "Rectifier_Image", "ATS_Image", "DG_Image" };
                }
                foreach (var item in images)
                {
                    var test = ObjPath.Where(x => x.Name.Split('.').First() == item).FirstOrDefault();
                    if (test == null)
                    {
                        ObjPath.Add(new ImagePathModel
                        {
                            Name = item,
                            Path = "",
                        });
                    }
                }

                return Json(ObjPath.ToList());
            }
            else
            {
                return Json("Images are not present for this PM");
            }
        }
        [Authorize]
        public ActionResult PMPlanningParameters()
        {
            if (!User.Identity.HasPermission("perm_pm_planning_parameters"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        

    }

}