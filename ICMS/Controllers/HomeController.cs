using ICMS.Common;
using ICMS.Common.Security;
using ICMS.CustomHelpers;
using ICMS.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ICMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Logger.LogAccess("Page Load");
            string userName = OAuthUtility.GetLoginId();
            StudentViewModel student = AuthenticateStudent(userName);

            if(student == null)
            {
                TempData["Unauthorized"] = true;
                return RedirectToAction("Unauthorized", "Home");
            }
            ViewBag.API_BASE_URL = Constants.APIEndPoint;
            ViewBag.WEBSITE_BASE_URL = Constants.WebsiteEndPoint;
            return View(student);
        }

        public ActionResult Unauthorized()
        {
            Logger.LogAccess("Page Load");
            if (TempData["Unauthorized"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Error()
        {
            Logger.LogAccess("Page Load");
            return View();
        }

        private StudentViewModel AuthenticateStudent(string userName)
        {
            StudentViewModel student = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["DirectAPIUrl"]);
                string token = OAuthUtility.ProtectAuthenticationTicket(OAuthUtility.GetAuthenticationTicket());
                Common.Logger.LogDebug("AuthenticateStudent token: " + token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseTask = client.GetAsync($"Auth/Authenticate/?userName={userName}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<StudentViewModel>();
                    readTask.Wait();

                    student = readTask.Result;
                }
            }
            return student;
        }
    }
}