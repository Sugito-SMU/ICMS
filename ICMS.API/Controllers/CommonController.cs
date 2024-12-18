using AutoMapper;
using ICMS.Entity.Entities;
using ICMS.Entity.ViewModels;
using ICMS.Manager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static ICMS.Common.ICMSConstants;

namespace ICMS.API.Controllers
{
    public class CommonController : ApiController
    {
        private readonly IActivityManager activityManager;

        public CommonController(IActivityManager activityManager)
        {
            this.activityManager = activityManager;
        }

        [HttpGet]
        [HttpOptions]
        public IHttpActionResult GetFAQLinks()
        {
            var viewModel = new FAQLinksViewModel() {
                CommunityService = ConfigurationManager.AppSettings["CommunityServiceFAQLink"],
                Internship = ConfigurationManager.AppSettings["InternshipFAQLink"]
            };
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { result = viewModel }));
        }
        
        [HttpGet]
        [HttpOptions]
        public IHttpActionResult GetActivityStatuses(string currentStatus)
        {
            IEnumerable<ActivityStatusViewModel> viewModelActivityStatus;
            IEnumerable<KeyValueDescription> statuses;

            statuses = activityManager.GetActivityStatusForChangeByAdmin(currentStatus);

            viewModelActivityStatus = Mapper.Map<IEnumerable<KeyValueDescription>, IEnumerable<ActivityStatusViewModel>>(statuses);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { result = viewModelActivityStatus }));
        }
    }
}
