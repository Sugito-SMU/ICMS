using System.Web;
using System.Web.Mvc;

namespace ICMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomErrorAttribute());
        }

        public class CustomErrorAttribute : HandleErrorAttribute
        {
            public override void OnException(ExceptionContext context)
            {
                Common.Logger.LogError(context.Exception);
                Common.Email.SendEmailForError(context.Exception, context.HttpContext.Request.Url.AbsoluteUri, null);
                HttpContext.Current.Response.Redirect("~/Home/Error");
            }
        }

    }
}
