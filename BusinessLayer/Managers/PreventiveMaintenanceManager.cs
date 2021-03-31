using BusinessLayer.Utilities;
using BusinessLayer.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLayer.Managers
{
    public class PreventiveMaintenanceManager
    {
        public static string getCookie(string cookie_name, string key_name)
        {
            try
            {
                System.Web.HttpCookie cookie = HttpContext.Current.Request.Cookies[cookie_name];
                string val = "";
                if (cookie != null)
                {
                    val = (!String.IsNullOrEmpty(cookie_name)) ? cookie[key_name] : cookie.Value;
                    if (!String.IsNullOrEmpty(val))
                    {
                        return Uri.UnescapeDataString(val);
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static void deleteCookie(string cookie_name)
        {
            System.Web.HttpCookie cookie = HttpContext.Current.Request.Cookies[cookie_name];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1d);
            }
        }
        public static System.Web.HttpCookie setCookie(string cookieName, string cookieValue, int timeOutInMin)
        {
            try
            {
                System.Web.HttpCookie cookie = new System.Web.HttpCookie(cookieName);
                cookie["LtpaToken2"] = cookieValue;
                cookie.Expires = DateTime.Now.AddMinutes(timeOutInMin);
                return cookie;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<RestResponseCookie> AuthenticateMaximo()
        {
            try
            {
                var url = System.Configuration.ConfigurationManager.AppSettings["MaximoApiPath"] + "/maximo/j_security_check";
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("j_username", "prime");
                request.AddParameter("j_password", "mobilink");
                client.FollowRedirects = false;
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Found)
                {

                    return response.Cookies.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string getAllTickets(string username)
        {
            try
            {
                //username = "NAIB.ULLAH";
                //username = "ZEESHAN.MANZOOR";
                username = username.ToUpper();
                var checkUser = ApiRequest("/maximo/oslc/script/prime.logormbu?userid=" + username, Method.GET, null, 0, "CheckUser");
                var selectClause = "oslc.select=*";
                var pageSize = "oslc.pageSize=100";
                string whereClause = "";
                string stageName = @"Reopen\Close";
                if (checkUser.Content == "MBU")
                {
                    whereClause = string.Format("oslc.where=stage in [\"Approval OAN\",\"Approval Logistics\",\"Reopen/Close\"] and approver=\"{0}\"", username);
                }
                else
                {
                    whereClause = string.Format("oslc.where=stage in [\"Resolution\",\"Reopen\"] and assignedto=\"{0}\"", username);
                }
                //var whereClause = "oslc.where=ttstatus=\"Open\"";
                var response = ApiRequest("/maximo/oslc/os/tprime?" + selectClause + "&" + whereClause + "&" + pageSize + "&_dropnulls=0", Method.GET, null, 0, null);
                //var response = ApiRequest("/maximo/oslc/os/prime?" + selectClause + "&" + whereClause + "&" + pageSize + "&_dropnulls=0", Method.GET, null, 0, null);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var parseJson = JObject.Parse(response.Content);
                    var data = parseJson["rdfs:member"].ToString();
                    return data;
                }
                else
                {
                    return response.Content;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string saveTicket(SaveTicketsVM model)
        {
            try
            {
                string json = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.None,
                                    new JsonSerializerSettings
                                    {
                                        NullValueHandling = NullValueHandling.Ignore
                                    });
                var response = ApiRequest("/maximo/oslc/os/tprime", Method.POST, json, 0, null);
                //var response = ApiRequest("/maximo/oslc/os/prime", Method.POST, json, 0, null);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return "success";
                }
                else
                {
                    return response.Content;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string ViewTicketDetails(int id)
        {
            try
            {
                var response = ApiRequest("/maximo/oslc/os/tprime/", Method.GET, null, id, null);
                //var response = ApiRequest("/maximo/oslc/os/tprime/", Method.GET, null, id, null);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Content;
                }
                else
                {
                    return response.Content;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string UpdateTicket(int id, SaveTicketsVM model, string viewName)
        {
            try
            {
                var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();
                if (viewName == "ViewTicketForApproval")
                {
                    jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:ttstatus");
                    jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:sitestatus");
                }
                jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:sitecode");
                jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:wapdafaults");
                jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:issue");
                jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:approver");
                jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:category");
                jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:approverstatustime");
                jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:createdate");
                jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:createby");
                jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:stage");
                jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:issuestarttime");
                if (viewName == "ViewTicketForApproval")
                {
                    if (model.approvalstatus == "Approved")
                    {
                        jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:rejectioncomments");
                    }
                }
                if (viewName == "ViewTicketForCloseReopenLogistics")
                {
                    if (model.approver != model.assignedto)
                    {
                        jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:issueclosetime");
                        jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:actiontaken");
                    }
                    jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:approvalstatus");
                    jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:sitestatus");
                    jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:statusdate");
                }
                if (viewName == "ViewTicketForResolution")
                {
                    jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:approvalstatus");
                    jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:rejectioncomments");
                }
                if (viewName == "ViewTicketForCloseReopenOAN")
                {
                    jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:approvalstatus");
                    jsonResolver.IgnoreProperty(typeof(SaveTicketsVM), "spi:sitestatus");
                }
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = jsonResolver;
                var json = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore,
                                    ContractResolver = jsonResolver
                                });
                var response = ApiRequest("/maximo/oslc/os/tprime/", Method.POST, json, id, "UPDATE");
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return "success";
                }
                else
                {
                    return response.Content;
                }
                //return "success";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string getReportData(GetTicketsVM model)
        {
            //StringBuilder whereClause = new StringBuilder();
            //if(model.sitecode != null || model.sitecode != "")
            //{
            //    whereClause.Append("sitecode=" + model.sitecode);
            //}
            //if(model.)
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            model.sitecode = model.sitecode ?? "";
            model.ttstatus = model.ttstatus ?? "";
            model.rbu = model.rbu ?? "";
            model.mbu = model.mbu ?? "";
            model.commregion = model.commregion ?? "";
            model.approvalstatus = model.approvalstatus ?? "";
            model.issueStartTimeFrom = model.issueStartTimeFrom ?? "";
            model.issueStartTimeTo = model.issueStartTimeTo ?? "";
            if (model.sitecode != "")
            {
                parameters.Add(new KeyValuePair<string, string>("sitecode", model.sitecode));
            }
            if (model.ttstatus != "")
            {
                parameters.Add(new KeyValuePair<string, string>("ttstatus", model.ttstatus));
            }
            if (model.rbu != "")
            {
                parameters.Add(new KeyValuePair<string, string>("rbu", model.rbu));
            }

            if (model.mbu != "")
            {
                parameters.Add(new KeyValuePair<string, string>("mbu", model.mbu));
            }
            if (model.commregion != "")
            {
                parameters.Add(new KeyValuePair<string, string>("commregion", model.commregion));
            }
            if (model.approvalstatus != "")
            {
                parameters.Add(new KeyValuePair<string, string>("approvalstatus", model.approvalstatus));
            }
            if (model.issueStartTimeFrom != "")
            {
                parameters.Add(new KeyValuePair<string, string>("issuestarttimeFrom", model.issueStartTimeFrom));
            }
            if (model.issueStartTimeTo != "")
            {
                parameters.Add(new KeyValuePair<string, string>("issuestarttimeTo", model.issueStartTimeTo));
            }

            StringBuilder where = new StringBuilder();
            for (int i = 0; i < parameters.Count(); i++)
            {
                if (i == 0)
                {
                    if (parameters[i].Key == "issuestarttimeFrom")
                    {
                        where.Append(string.Format("issuestarttime>=\"{0}\"", parameters[i].Value));
                    }
                    //else if (parameters[i].Key == "issuestarttimeTo")
                    //{
                    //    where.Append(string.Format(" and issuestarttime<\"{0}\"", parameters[i].Value));
                    //}
                    else
                    {
                        where.Append(string.Format(parameters[i].Key + "=\"{0}\"", parameters[i].Value));
                    }
                }
                else
                {
                    if (parameters[i].Key == "issuestarttimeFrom")
                    {
                        where.Append(string.Format(" and issuestarttime>=\"{0}\"", parameters[i].Value));
                    }
                    else if (parameters[i].Key == "issuestarttimeTo")
                    {
                        where.Append(string.Format(" and issuestarttime<=\"{0}\"", parameters[i].Value));
                    }
                    else
                    {
                        where.Append(string.Format(" and " + parameters[i].Key + "=\"{0}\"", parameters[i].Value));
                    }
                }
            }
            var whereClause = "oslc.where=" + where.ToString();
            //string whereClause = string.Format("sitecode=\"{0}\" and ttstatus=\"{1}\" and rbu=\"{2}\" and mbu=\"{3}\" and comregion=\"{4}\" and approvalstatus=\"{5}\"",model.sitecode,model.ttstatus,model.rbu,model.mbu,model.comregion,model.approvalstatus);
            var selectClause = "oslc.select=*";
            //var pageSize = "oslc.pageSize=100";
            var orderBy = "oslc.orderBy=-issuestarttime";
            var response = ApiRequest("/maximo/oslc/os/tprime?" + selectClause + "&" + whereClause + "&_dropnulls=0", Method.GET, null, 0, null);
            //var response = ApiRequest("/maximo/oslc/os/prime?" + selectClause + "&" + whereClause + "&_dropnulls=0", Method.GET, null, 0, null);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var parseJson = JObject.Parse(response.Content);
                var data = parseJson["rdfs:member"].ToString();
                return data;
            }
            else
            {
                return response.Content;
            }
        }
        private static IRestResponse ApiRequest(string apiEndPoint, Method requestType, string body, int parameter, string apiName)
        {
            try
            {
                string cookieValue = getCookie("LtpaToken2", "LtpaToken2");
                var url = "";
                if (parameter > 0)
                {
                    url = System.Configuration.ConfigurationManager.AppSettings["MaximoApiPath"] + apiEndPoint + parameter;
                }
                else
                {
                    url = System.Configuration.ConfigurationManager.AppSettings["MaximoApiPath"] + apiEndPoint;
                }
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(requestType);
                if (body != null)
                {
                    request.AddJsonBody(body);
                }
                request.AddParameter("LtpaToken2", cookieValue, ParameterType.Cookie);
                if (apiName != "CheckUser")
                {
                    request.AddHeader("Content-Type", "application/json");
                }
                if (apiName == "UPDATE")
                {
                    request.AddHeader("x-method-override", "PATCH");
                }
                IRestResponse response = client.Execute(request);
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
