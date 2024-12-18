using ICMS.Common;
using ICMS.Common.Security;
using ICMS.Entity.Entities;
using ICMS.Entity.ViewModels;
using ICMS.Manager;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Reflection;
using System;

namespace ICMS.API
{
    public class AuthorizeAPI : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ControllerContext.Request.Method != HttpMethod.Options)
            {
                string userName = OAuthUtility.GetLoginId();
                Student student;
                if (actionContext.ControllerContext.Controller is Controllers.ActivitiesController)
                {
                    student = ((Controllers.ActivitiesController)actionContext.ControllerContext.Controller).studentManager.GetStudentByUsername(userName);
                }
                else
                {
                    student = ((Controllers.LearningObjectiveController)actionContext.ControllerContext.Controller).studentManager.GetStudentByUsername(userName);
                }

                if (student == null)
                {
                    Admin admin;
                    if (actionContext.ControllerContext.Controller is Controllers.ActivitiesController)
                    {
                        admin = ((Controllers.ActivitiesController)actionContext.ControllerContext.Controller).adminManager.GetAdminByUsername(userName);
                        Logger.LogAccess("Admin Role");
                    }
                    else
                    {
                        admin = ((Controllers.LearningObjectiveController)actionContext.ControllerContext.Controller).adminManager.GetAdminByUsername(userName);
                        Logger.LogAccess("Admin Role");
                    }

                    if (admin == null || !ICMSConstants.AdminActions.Contains(actionContext.ActionDescriptor.ActionName))
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                        Logger.LogAccess("Unauthorized");
                    }
                }
                else
                {
                    Logger.LogDebug(string.Format("actionContext.ActionArguments[studentId] = {0}", actionContext.ActionArguments["studentId"]));
                    Logger.LogDebug(string.Format("student.StudentId = {0}", student.StudentId));
                    Logger.LogAccess("Student Role");
                    if ((string)actionContext.ActionArguments["studentId"] != student.StudentId)
                    {
                        Logger.LogAccess("Unauthorized");
                        throw new ArgumentException("Unauthorized");
                    }
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
        }
    }

    public class SanitizeAPI : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.Count > 0)
            {
                string[] keys = new string[actionContext.ActionArguments.Count];
                actionContext.ActionArguments.Keys.CopyTo(keys, 0);
                foreach (string key in keys)
                {
                    if (actionContext.ActionArguments[key] != null)
                    {
                        if (actionContext.ActionArguments[key] is string)
                        {
                            Logger.LogDebug("SanitizeAPI - action|key|value: " + actionContext.Request.RequestUri.LocalPath + "|" + key + "|" + actionContext.ActionArguments[key].ToString());
                            actionContext.ActionArguments[key] = ICMSSanitizer.Sanitize(actionContext.ActionArguments[key].ToString());
                        }
                        else if (actionContext.ActionArguments[key] is ActivityUpdateViewModel)
                        {
                            SinitizeComplexObjects((ActivityUpdateViewModel)actionContext.ActionArguments[key]);
                        }
                    }
                }
            }
        }

        void SinitizeComplexObjects(object model)
        {
            foreach (PropertyInfo property in model.GetType().GetProperties())
            {
                if (property.GetValue(model) != null)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(model, ICMSSanitizer.Sanitize(property.GetValue(model).ToString()));
                    }
                    else if (property.PropertyType == typeof(List<LearningObjectiveViewModel>))
                    {
                        foreach (LearningObjectiveViewModel lo in (List<LearningObjectiveViewModel>)property.GetValue(model))
                        {
                            SinitizeComplexObjects(lo);
                        }
                    }
                    else if (property.PropertyType == typeof(List<ActivityQuestionAnswerViewModel>))
                    {
                        foreach (ActivityQuestionAnswerViewModel lo in (List<ActivityQuestionAnswerViewModel>)property.GetValue(model))
                        {
                            SinitizeComplexObjects(lo);
                        }
                    }
                    else if (property.PropertyType == typeof(List<LearningObjectiveQuestionAnswerViewModel>))
                    {
                        foreach (LearningObjectiveQuestionAnswerViewModel lo in (List<LearningObjectiveQuestionAnswerViewModel>)property.GetValue(model))
                        {
                            SinitizeComplexObjects(lo);
                        }
                    }
                }
            }
        }
    }

    public class AuthenticateAPI : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ControllerContext.Request.Method != HttpMethod.Options)
            {
                string userName = OAuthUtility.GetLoginId();
                if (string.IsNullOrEmpty(userName))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
        }
    }

    public class AdminAPI : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ControllerContext.Request.Method != HttpMethod.Options)
            {
                string userName = OAuthUtility.GetLoginId();

                Admin admin;
                if (actionContext.ControllerContext.Controller is Controllers.ActivitiesController)
                {
                    admin = ((Controllers.ActivitiesController)actionContext.ControllerContext.Controller).adminManager.GetAdminByUsername(userName);
                    Logger.LogAccess("Admin Role");
                }
                else
                {
                    admin = ((Controllers.LearningObjectiveController)actionContext.ControllerContext.Controller).adminManager.GetAdminByUsername(userName);
                    Logger.LogAccess("Admin Role");
                }

                if (admin == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                    Logger.LogAccess("Unauthorized");
                }
            }
        }
    }
}
