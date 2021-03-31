using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModels
{
    public class SaveTicketsVM
    {

        [JsonProperty("spi:ticketid")]
        public string ticketid { get; set; }
        [JsonProperty("spi:ttapproval")]
        public string ttapproval { get; set; }
        [JsonProperty("spi:assignedto")]
        public string assignedto { get; set; }
        [JsonProperty("spi:sitecode")]
        public string sitecode { get; set; }
        [JsonProperty("spi:changeby")]
        public string changeby { get; set; }
        [JsonProperty("spi:issueclosetime")]
        public string issueclosetime { get; set; }
        [JsonProperty("spi:wapdafaults")]
        public string wapdafaults { get; set; }
        [JsonProperty("spi:actiontaken")]
        public string actiontaken{ get; set; }
        [JsonProperty("spi:sitestatus")]
        public string sitestatus { get; set; }
        [JsonProperty("spi:createdby")]
        public string createdby { get; set; }
        [JsonProperty("spi:statusdate")]
        public string statusdate { get; set; }
        [JsonProperty("spi:description")]
        public string description { get; set; }
        [JsonProperty("spi:ttstatus")]
        public string ttstatus { get; set; }
        [JsonProperty("spi:reason")]
        public string reason { get; set; }
        [JsonProperty("spi:issuestarttime")]
        public string issuestarttime { get; set; }
        [JsonProperty("spi:issue")]
        public string issue { get; set; }
        [JsonProperty("spi:approver")]
        public string approver { get; set; }
        [JsonProperty("spi:category")]
        public string category { get; set; }
        [JsonProperty("spi:approverstatustime")]
        public string approverstatustime { get; set; }
        [JsonProperty("spi:changedate")]
        public string changedate { get; set; }
        [JsonProperty("spi:createdate")]
        public string createdate { get; set; }
        [JsonProperty("spi:approvalstatus")]
        public string approvalstatus { get; set; }
        [JsonProperty("spi:rejectioncomments")]
        public string rejectioncomments { get; set; }
        [JsonProperty("spi:stage")]
        public string stage { get; set; }
    }

    public class GetTicketsVM
    {

        [JsonProperty("spi:ticketid")]
        public string ticketid { get; set; }
        [JsonProperty("spi:ttapproval")]
        public string ttapproval { get; set; }
        [JsonProperty("spi:sitecode")]
        public string sitecode { get; set; }
        [JsonProperty("spi:ttstatus_description")]
        public string ttstatus_description { get; set; }
        [JsonProperty("spi:changeby")]
        public string changeby { get; set; }
        [JsonProperty("spi:issueclosetime")]
        public string issueclosetime { get; set; }
        [JsonProperty("spi:wapdafaults")]
        public string wapdafaults { get; set; }
        [JsonProperty("spi:actiontaken")]
        public string actiontaken { get; set; }
        [JsonProperty("spi:sitestatus")]
        public string sitestatus { get; set; }
        [JsonProperty("spi:createdby")]
        public string createdby { get; set; }
        [JsonProperty("spi:statusdate")]
        public string statusdate { get; set; }
        [JsonProperty("spi:description")]
        public string description { get; set; }
        [JsonProperty("spi:ttstatus")]
        public string ttstatus { get; set; }
        [JsonProperty("spi:reason")]
        public string reason { get; set; }
        [JsonProperty("spi:issuestarttime")]
        public string issuestarttime { get; set; }
        [JsonProperty("spi:issue")]
        public string issue { get; set; }
        [JsonProperty("spi:approver")]
        public string approver { get; set; }
        [JsonProperty("spi:category")]
        public string category { get; set; }
        [JsonProperty("spi:approverstatustime")]
        public string approverstatustime { get; set; }
        [JsonProperty("spi:changedate")]
        public string changedate { get; set; }
        [JsonProperty("spi:primeid")]
        public int primeid { get; set; }
        [JsonProperty("spi:createdate")]
        public string createdate { get; set; }
        [JsonProperty("spi:approvalstatus")]
        public string approvalstatus { get; set; }
        [JsonProperty("spi:rejectioncomments")]
        public string rejectioncomments { get; set; }
        [JsonProperty("spi:stage")]
        public string stage { get; set; }
        [JsonProperty("spi:assignedto")]
        public string assignedto { get; set; }
        [JsonProperty("spi:mbu")]
        public string mbu { get; set; }
        [JsonProperty("spi:rbu")]
        public string rbu { get; set; }
        [JsonProperty("spi:commregion")]
        public string commregion { get; set; }
        [JsonProperty("spi:aging")]
        public string aging { get; set; }
        public string issueStartTimeFrom { get; set; }
        public string issueStartTimeTo { get; set; }
    }
}
