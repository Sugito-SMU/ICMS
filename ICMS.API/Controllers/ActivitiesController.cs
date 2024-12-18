using AutoMapper;
using ICMS.Common;
using ICMS.Entity.Entities;
using ICMS.Entity.ViewModels;
using ICMS.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static ICMS.Common.ICMSConstants;

namespace ICMS.API.Controllers
{
    [CustomExceptionFilter]
    [AuthorizeAPI]
    [SanitizeAPI]
    public class ActivitiesController : ApiController
    {
        private readonly IActivityManager activityManager;
        private readonly IQuestionManager questionManager;
        private readonly IOverallSummaryManager overallsummaryManager;
        internal readonly IStudentManager studentManager;
        internal readonly IAdminManager adminManager;
        private readonly IStudentReflectionHeaderManager studentReflectionHeaderManager;
        private readonly ILearningObjectiveAnswerManager learningobjectiveanswerManager;
        private readonly ILearningObjectiveStatusManager learningobjectivestatusManager;

        public ActivitiesController(IActivityManager activityManager, IOverallSummaryManager overallsummaryManager, IQuestionManager questionManager,
            IStudentManager studentManager, IAdminManager adminManager, IStudentReflectionHeaderManager studentReflectionHeaderManager,
            ILearningObjectiveAnswerManager learningobjectiveanswerManager, ILearningObjectiveStatusManager learningobjectivestatusManager)
        {
            this.activityManager = activityManager;
            this.questionManager = questionManager;
            this.overallsummaryManager = overallsummaryManager;
            this.studentReflectionHeaderManager = studentReflectionHeaderManager;
            this.studentManager = studentManager;
            this.adminManager = adminManager;
            this.learningobjectiveanswerManager = learningobjectiveanswerManager;
            this.learningobjectivestatusManager = learningobjectivestatusManager;
        }

        [HttpGet]
        [HttpOptions]
        public IHttpActionResult GetActivitiesByType(string studentId, string typeCode)
        {
            IEnumerable<ActivityListViewModel> viewModelActivities;
            IEnumerable<Activity> activities;

            activities = activityManager.GetActivities(studentId, typeCode);

            viewModelActivities = Mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityListViewModel>>(activities);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { result = viewModelActivities }));
        }

        [HttpGet]
        [HttpOptions]
        public IHttpActionResult GetOverallSummaryByType(string studentId, string typeCode)
        {
            IEnumerable<OverallSummaryViewModel> viewModelSummaries;
            IEnumerable<OverallSummary> summaries;

            summaries = overallsummaryManager.GetOverallSummary(studentId, typeCode);

            viewModelSummaries = Mapper.Map<IEnumerable<OverallSummary>, IEnumerable<OverallSummaryViewModel>>(summaries);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { result = viewModelSummaries }));
        }

        [HttpGet]
        [HttpOptions]
        public IHttpActionResult getActivityDetails(string studentId, string id)
        {
            ActivityViewModel viewModelActivity;
            Activity activity;

            activity = activityManager.GetActivity(id, studentId);

            viewModelActivity = Mapper.Map<Activity, ActivityViewModel>(activity);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, viewModelActivity));
        }

        [HttpGet]
        [HttpOptions]
        public IHttpActionResult getActivityQuestions(string studentId, string activityId)
        {
            IEnumerable<ActivityQuestionAnswerViewModel> viewModelActivityQuestionAnswer;
            IEnumerable<ActivityQuestion> activityQuestion;

            activityQuestion = questionManager.GetActivityQuestions(studentId, activityId);

            viewModelActivityQuestionAnswer = Mapper.Map<IEnumerable<ActivityQuestion>, IEnumerable<ActivityQuestionAnswerViewModel>>(activityQuestion);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { result = viewModelActivityQuestionAnswer }));
        }

        [HttpGet]
        [HttpOptions]
        public IHttpActionResult getPostReflectionHeader(string studentId, string activityId)
        {
            string header = activityManager.GetPostReflectionHeader(studentId, activityId);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, header));
        }

        [HttpPost]
        [HttpOptions]
        [AdminAPI]
        public IHttpActionResult UpdateActivityStatus(ActivityStatusUpdateViewModel model)
        {
            //set student global variables for use in Automapper & CreatedModifiedBaseEntity
            Student studentDetails = studentManager.GetStudentById(model.StudentId);
            GlobalVariables.StudentId = studentDetails.StudentId;
            GlobalVariables.StudentUsername = studentDetails.NtLoginId;

            //questionManager.DeleteAnswers(model.ActivityId, model.StudentId);
            //questionManager.SaveQuestions();
            //learningobjectiveanswerManager.DeleteAnswers(model.ActivityId, model.StudentId, model.Status == ActivityReflectionCodes.PreActivityDraft ? ActivityStageCode.PRE : (model.Status == ActivityReflectionCodes.MidActivityDraft ? ActivityStageCode.MID : ActivityStageCode.POST) );
            //learningobjectiveanswerManager.SaveLearningObjectiveAnswer();
            //learningobjectivestatusManager.ResetLearningObjectiveStatus(model.ActivityId, model.StudentId, model.Status);
            //learningobjectivestatusManager.SaveLearningObjectiveStatus();

            studentReflectionHeaderManager.UpdateActivityStatus(model.ActivityId, model.StudentId, model.Status);
            studentReflectionHeaderManager.SaveStudentReflectionHeader();
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, true));
        }
    }
}