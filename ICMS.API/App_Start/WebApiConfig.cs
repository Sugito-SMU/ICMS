using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Filters;
using ICMS.Common;

namespace ICMS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new CustomExceptionFilterAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            string errMsg = "System encountered an error. Please contact IT Help Centre (helpdesk@smu.edu.sg) for assistance.";
            var response = new System.Net.Http.HttpResponseMessage
            {
                StatusCode = (System.Net.HttpStatusCode)801,
                ReasonPhrase = errMsg,
                Content = new System.Net.Http.StringContent(errMsg)
                //Content = new System.Net.Http.StringContent(context.Exception.ToString())
            };
            Common.Logger.LogError(context.Exception);
            Common.Email.SendEmailForError(context.Exception, context.Request.RequestUri.AbsoluteUri, null);

            throw new HttpResponseException(response);
        }
    }

}
