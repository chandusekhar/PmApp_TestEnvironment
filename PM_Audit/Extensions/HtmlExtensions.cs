using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PM_Audit.Extensions
{
    public static class HtmlExtensions
    {
        public static string BuildBreadcrumbNavigation(this HtmlHelper helper)
        {
            var Controller_Name = helper.ViewContext.ParentActionViewContext.RouteData.GetRequiredString("controller");
            var Action_Name = helper.ViewContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            var checkParameter = helper.ViewContext.ParentActionViewContext.RouteData.Values.ContainsKey("id");
            int paramValue = 0;
            if (checkParameter)
            {
                paramValue = int.Parse(helper.ViewContext.ParentActionViewContext.RouteData.GetRequiredString("id"));
            }
            StringBuilder breadcrumb = new StringBuilder("<ol class='breadcrumb'><li>").Append(helper.ActionLink("Dashboard", "Index", "Dashboard").ToHtmlString()).Append("<i class='fa fa - dashboard'></i></li>");
            if (Controller_Name == "Dashboard")
            {
                return breadcrumb.Append("</ol>").ToString();
            }
            var ControllerDisplayName = Controller_Name.ToLower();
            var ActionDisplayName = Action_Name.ToLower();
            #region [Setting Up Controller Names]
            if (ControllerDisplayName == "preventivemaintenance")
            {
                ControllerDisplayName = "Preventive Maintenance";
            }

            if (ControllerDisplayName == "alarmmanagement")
            {
                ControllerDisplayName = "Alarm Management";
            }
            if (ControllerDisplayName == "inventorymanagement")
            {
                ControllerDisplayName = "Inventory Management";
            }
            if (ControllerDisplayName == "schedulemanagement")
            {
                ControllerDisplayName = "PM Schedule Management";
            }
            if (ControllerDisplayName == "admin")
            {
                ControllerDisplayName = "Admin Module";
            }
            if (ControllerDisplayName == "reports")
            {
                ControllerDisplayName = "Reports";
            }
            if (ControllerDisplayName == "changepassword")
            {
                ControllerDisplayName = "Change Password";
            }
            if (ControllerDisplayName == "profilemanagement")
            {
                ControllerDisplayName = "Profile Management";
            }
            if (ControllerDisplayName == "audit")
            {
                ControllerDisplayName = "Audit";
            }
            if (ControllerDisplayName == "primetprime")
            {
                ControllerDisplayName = "Prime TPrime";
            }
            #endregion


            #region [Setting Up Action Names]

            if (ActionDisplayName == "viewpm")
            {
                ActionDisplayName = "View PM";
            }
            if (ActionDisplayName == "closepm")
            {
                ActionDisplayName = "Close PM Telco";
            }
            if (ActionDisplayName == "closepm_nontelco")
            {
                ActionDisplayName = "Close PM Non Telco";
            }
            if (ActionDisplayName == "viewinventory")
            {
                ActionDisplayName = "View NON-Telco Inventory";
            }
            if (ActionDisplayName == "viewtelcoinventory")
            {
                ActionDisplayName = "View Telco Inventory";
            }
            if (ActionDisplayName == "viewschedule")
            {
                ActionDisplayName = "View Schedule";
            }
            if (ActionDisplayName == "extractreports")
            {
                ActionDisplayName = "Extract Reports";
            }
            if (ActionDisplayName == "checklistreport")
            {
                ActionDisplayName = "PM checklist Reports";
            }
            if (ActionDisplayName == "pmpendingapproval")
            {
                ActionDisplayName = "PM Pending Approval Report";
            }
            if (ActionDisplayName == "inventoryreport")
            {
                ActionDisplayName = "Inventory Reports";
            }
            if (ActionDisplayName == "inventorypendingapproval")
            {
                ActionDisplayName = "Inventory Pending Approval Report";
            }
            if (ActionDisplayName == "nonpmreport")
            {
                ActionDisplayName = "NoN PM Sites Report";
            }
            if (ActionDisplayName == "drilldownreport")
            {
                ActionDisplayName = "PM Site Outage Report";
            }
            if (ActionDisplayName == "viewtelcopmdetails")
            {
                ActionDisplayName = "Telco PM Details";
            }
            if (ActionDisplayName == "viewnontelcopmdetails")
            {
                ActionDisplayName = "Non Telco PM Details";
            }
            if (ActionDisplayName == "viewpmforapproval")
            {
                ActionDisplayName = "PM for approval";
            }
            if (ActionDisplayName == "viewtelcopmforapproval")
            {
                ActionDisplayName = "Telco PM for approval";
            }
            if (ActionDisplayName == "viewnontelcopmforapproval")
            {
                ActionDisplayName = "Non Telco PM for approval";
            }
            if (ActionDisplayName == "inventorydetails")
            {
                ActionDisplayName = "Inventory Details";
            }
            if (ActionDisplayName == "viewtelcopmdetailsforaudit")
            {
                ActionDisplayName = "Telco Details";
            }
            if (ActionDisplayName == "viewnontelcopmdetailsforaudit")
            {
                ActionDisplayName = "NonTelco Details";
            }
            if (ActionDisplayName == "audit_reports")
            {
                ActionDisplayName = "Audit Reports";
            }
            if (ActionDisplayName == "viewcomparison")
            {
                ActionDisplayName = "Analysis Reports";
            }
            if (ActionDisplayName == "viewauditdetailsreport")
            {
                ActionDisplayName = "Audit Details Report";
            }
            if (ActionDisplayName == "viewauditdetails")
            {
                ActionDisplayName = "Audit Details";
            }
            if (ActionDisplayName == "specifficinventoryiddetails")
            {
                ActionDisplayName = "Inventory Details";
            }
            if (ActionDisplayName == "viewgeneralauditdetailsreport")
            {
                ActionDisplayName = "General Audit Details Report";
            }
            if (ActionDisplayName == "generalaudittelco")
            {
                ActionDisplayName = "General Audit Telco";
            }
            if (ActionDisplayName == "generalauditnontelco")
            {
                ActionDisplayName = "General Audit NON Telco";
            }
            if (ActionDisplayName == "viewgeneralauditdetails")
            {
                ActionDisplayName = "View General Audit Details";
            }
            if (ActionDisplayName == "viewticket")
            {
                ActionDisplayName = "View Tickets";
            }
            if (ActionDisplayName == "openticket")
            {
                ActionDisplayName = "Open Ticket";
            }
            if (ActionDisplayName == "viewticketforapproval")
            {
                ActionDisplayName = "View Ticket For Approval";
            }
            if (ActionDisplayName == "viewticketforclosereopen")
            {
                ActionDisplayName = "View Ticket For Close/Reopen";
            }
            if (ActionDisplayName == "viewticketforResolution")
            {
                ActionDisplayName = "View Ticket For Resolution";
            }
            if (ActionDisplayName == "report")
            {
                ActionDisplayName = "Report";
            }

            #endregion





            breadcrumb.Append("<li>");
            breadcrumb.Append(helper.ActionLink(ControllerDisplayName,
                                               "Index",
                                               Controller_Name));
            breadcrumb.Append("</li>");

            if (Action_Name.ToLower() != "index")
            {
                if (Action_Name.ToLower() == "viewticketforapproval" || Action_Name.ToLower() == "viewticketforclosereopen" || Action_Name.ToLower() == "viewticketforresolution")
                {
                    breadcrumb.Append("<li>");
                    breadcrumb.Append(helper.ActionLink("View Tickets", "ViewTicket", "PrimeTPrime"));
                    breadcrumb.Append("</li>");
                    breadcrumb.Append("<li>");
                    breadcrumb.Append(helper.ActionLink(ActionDisplayName,
                                                        Action_Name,
                                                        Controller_Name, new { id = paramValue }, null));
                    breadcrumb.Append("</li>");
                }
                else
                {
                    breadcrumb.Append("<li>");
                    breadcrumb.Append(helper.ActionLink(ActionDisplayName,
                                                        Action_Name,
                                                        Controller_Name));
                    breadcrumb.Append("</li>");
                }
            }
            if (Action_Name.ToLower() == "viewticketforapproval" || Action_Name.ToLower() == "viewticketforclosereopen" || Action_Name.ToLower() == "viewticketforresolution")
            {
                breadcrumb.Append("<li>");
            }

            return breadcrumb.Append("</ol>").ToString();
        }
    }
}