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
using ICMS.API.Validation;

namespace ICMS.API.Controllers
{
    [CustomExceptionFilter]
    [AuthorizeAPI]
    [SanitizeAPI]
    public class LearningObjectiveController : ApiController
    {
        private readonly ILearningObjectiveManager learningobjectiveManager;
        private readonly ILearningObjectiveStatusManager learningobjectivestatusManager;
        private readonly ILearningObjectiveAnswerManager learningobjectiveanswerManager;
        private readonly IQuestionManager questionManager;
        private readonly IStudentReflectionHeaderManager studentReflectionHeaderManager;
        private readonly IActivityManager activityManager;
        public readonly IStudentManager studentManager;
        public readonly IAdminManager adminManager;

        public LearningObjectiveController(ILearningObjectiveManager learningobjectiveManager,
            ILearningObjectiveStatusManager learningobjectivestatusManager, ILearningObjectiveAnswerManager learningobjectiveanswerManager,
            IQuestionManager questionManager, IStudentReflectionHeaderManager studentReflectionHeaderManager, IActivityManager activityManager,
            IStudentManager studentManager, IAdminManager adminManager)
        {
            this.learningobjectiveManager = learningobjectiveManager;
            this.learningobjectivestatusManager = learningobjectivestatusManager;
            this.learningobjectiveanswerManager = learningobjectiveanswerManager;
            this.questionManager = questionManager;
            this.studentReflectionHeaderManager = studentReflectionHeaderManager;
            this.activityManager = activityManager;
            this.studentManager = studentManager;
            this.adminManager = adminManager;
        }

        [HttpGet]
        [HttpOptions]
        public IHttpActionResult getLearningObjectives(string studentId, string activityId, string stage)
        {
            IEnumerable<LearningObjectiveViewModel> viewModellLarningObjectives;
            IEnumerable<LearningObjective> learningObjectives;

            learningObjectives = learningobjectiveManager.GetLearningObjectives(studentId, activityId, stage);

            viewModellLarningObjectives = Mapper.Map<IEnumerable<LearningObjective>, IEnumerable<LearningObjectiveViewModel>>(learningObjectives);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { result = viewModellLarningObjectives }));
        }

        [HttpPost]
        [HttpOptions]
        public IHttpActionResult UpdateActivityLearningObjectives(string studentId, ActivityUpdateViewModel model)
        {
            //Decode from base64 first
            if (model.LearningObjectives != null)
            {
                for (int i = 0; i < model.LearningObjectives.Count; i++)
                {
                    var lo = model.LearningObjectives[i];
                    if (lo.LearningObjectiveQuestions != null)
                    {
                        for (int j = 0; j < lo.LearningObjectiveQuestions.Count; j++)
                        {
                            var loa = lo.LearningObjectiveQuestions[j];
                            if (!string.IsNullOrEmpty(loa.Answer))
                            {
                                loa.Answer = loa.Answer.Replace("&#43;", "+").Replace("&#61;", "=").Replace("&#47;", "/");
                                loa.Answer = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(loa.Answer));
                            }
                        }
                    }
                    if (lo.LearningObjectiveStageQuestionAnswers != null)
                    {
                        for (int k = 0; k < lo.LearningObjectiveStageQuestionAnswers.Count; k++)
                        {
                            var loqa = lo.LearningObjectiveStageQuestionAnswers[k];
                            if (!string.IsNullOrEmpty(loqa.Answer))
                            {
                                loqa.Answer = loqa.Answer.Replace("&#43;", "+").Replace("&#61;", "=").Replace("&#47;", "/");
                                loqa.Answer = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(loqa.Answer));
                            }
                        }
                    }
                }
            }
            if (model.ActivityQuestionAnswers != null)
            {
                for (int m = 0; m < model.ActivityQuestionAnswers.Count; m++)
                {
                    var postQn = model.ActivityQuestionAnswers[m];
                    if (!string.IsNullOrEmpty(postQn.Answer))
                    {
                        postQn.Answer = postQn.Answer.Replace("&#43;", "+").Replace("&#61;", "=").Replace("&#47;", "/");
                        postQn.Answer = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(postQn.Answer));
                    }
                }
            }

            bool result = false;

            model.LearningObjectives.ForEach(lo => { lo.IsSelected = model.Stage != ActivityStageCode.PRE ? null : lo.IsSelected; });

            if (!SaveValidation.IsValid(model) || (model.Submit && !SubmitValidation.IsValid(model)))
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, result));
            }

            //set student global variables for use in Automapper & CreatedModifiedBaseEntity
            Student studentDetails = studentManager.GetStudentById(studentId);
            GlobalVariables.StudentId = studentDetails.StudentId;
            GlobalVariables.StudentUsername = studentDetails.NtLoginId;


            //mapping should be after setting student global variables so that mapper can get student id from global variables
            Activity activity = Mapper.Map<ActivityUpdateViewModel, Activity>(model);

            Activity activityDetails = activityManager.GetActivity(activity.ActivityId, studentId);

            switch (model.Stage)
            {
                case ActivityStageCode.PRE:
                    if (ICMSMethods.ShowPreActivity(activityDetails.OverallSummaryStatus, activityDetails.StatusCode, activityDetails.StartDate, activityDetails.Url)
                        && ICMSMethods.EnablePreActivity(activityDetails.OverallSummaryStatus, activityDetails.StatusCode, activityDetails.StartDate, activityDetails.ProBonoRecognised > 0, activityDetails.Url))
                    {
                        //delete unchecked learningobjectives and add selected learningobjectives
                        learningobjectivestatusManager.DeleteMany(activity.LearningObjectives.Where(lo => lo.LearningObjectiveStatus == null).Select(lo => new LearningObjectiveStatus() { ActivityId = activity.ActivityId, LearningObjectiveId = lo.LearningObjectiveId, StudentId = studentId }).ToList());
                        learningobjectivestatusManager.SaveLearningObjectiveStatus();
                        learningobjectivestatusManager.AddLearningObjectiveStatus(activity.LearningObjectives.Where(lo => lo.LearningObjectiveStatus != null).Select(lo => new LearningObjectiveStatus() { ActivityId = activity.ActivityId, LearningObjectiveId = lo.LearningObjectiveId, StudentId = studentId }).ToList());
                        learningobjectivestatusManager.SaveLearningObjectiveStatus();

                        //remove answers of unchecked learningobjectives and add for selected learningobjectives
                        learningobjectiveanswerManager.DeleteMany(activity.LearningObjectives.Where(lo => lo.LearningObjectiveStatus == null).Select(loq => loq.LearningObjectiveQuestions).SelectMany(q => q).Select(a => { a.LearningObjectiveAnswer.ActivityId = activity.ActivityId; return a.LearningObjectiveAnswer; }).ToList());
                        learningobjectiveanswerManager.SaveLearningObjectiveAnswer();
                        learningobjectiveanswerManager.DeleteMidAndPostAnswers(activity.LearningObjectives.Where(lo => lo.LearningObjectiveStatus == null).Select(lo => new LearningObjectiveStatus() { ActivityId = activity.ActivityId, LearningObjectiveId = lo.LearningObjectiveId, StudentId = studentId }).ToList());
                        learningobjectiveanswerManager.SaveLearningObjectiveAnswer();
                        learningobjectiveanswerManager.AddLearningObjectiveQuestionAnswers(activity.LearningObjectives.Where(lo => lo.LearningObjectiveStatus != null).Select(lo => lo.LearningObjectiveQuestions).SelectMany(qa => qa).Select(qa => { qa.LearningObjectiveAnswer.ActivityId = activity.ActivityId; return qa.LearningObjectiveAnswer; }).ToList());
                        learningobjectiveanswerManager.SaveLearningObjectiveAnswer();

                        studentReflectionHeaderManager.UpdateActivityStatus(activity.ActivityId, studentId, model.Submit ? ActivityReflectionCodes.PreActivitySubmitted : ActivityReflectionCodes.PreActivityDraft);
                        studentReflectionHeaderManager.SaveStudentReflectionHeader();
                        result = true;
                    }
                    break;
                case ActivityStageCode.MID:
                    if (ICMSMethods.ShowMidActivity(activityDetails.OverallSummaryStatus, activityDetails.StatusCode, activityDetails.StartDate, activityDetails.EndDate, activityDetails.Url)
                        && ICMSMethods.EnableMidActivity(activityDetails.OverallSummaryStatus, activityDetails.StatusCode, activityDetails.StartDate, activityDetails.EndDate, activityDetails.Url))
                    {
                        //update if objective is on track or not
                        learningobjectivestatusManager.UpdateLearningObjectiveStatus(activity.LearningObjectives.Select(lo => lo.LearningObjectiveStatus).ToList(), model.Stage);
                        learningobjectivestatusManager.SaveLearningObjectiveStatus();

                        //update answers of objective not on track questions
                        learningobjectiveanswerManager.DeleteMany(activity.LearningObjectives.Where(lo => lo.LearningObjectiveStatus.IsMidActivityOnTarget == (bool?)true).SelectMany(lo => lo.LearningObjectiveQuestions).Select(loq => { loq.LearningObjectiveAnswer.ActivityId = activity.ActivityId; return loq.LearningObjectiveAnswer; }).ToList());
                        learningobjectiveanswerManager.SaveLearningObjectiveAnswer();
                        learningobjectiveanswerManager.AddLearningObjectiveQuestionAnswers(activity.LearningObjectives.Where(lo => lo.LearningObjectiveStatus.IsMidActivityOnTarget == (bool?)false).SelectMany(lo => lo.LearningObjectiveQuestions).Select(loq => { loq.LearningObjectiveAnswer.ActivityId = activity.ActivityId; return loq.LearningObjectiveAnswer; }).ToList());
                        learningobjectiveanswerManager.SaveLearningObjectiveAnswer();

                        studentReflectionHeaderManager.UpdateActivityStatus(activity.ActivityId, studentId, model.Submit ? ActivityReflectionCodes.MidActivitySubmitted : ActivityReflectionCodes.MidActivityDraft);
                        studentReflectionHeaderManager.SaveStudentReflectionHeader();
                        result = true;
                    }
                    break;
                case ActivityStageCode.POST:
                    if (ICMSMethods.ShowPostActivity(activityDetails.OverallSummaryStatus, activityDetails.StatusCode, activityDetails.StartDate, activityDetails.EndDate, activityDetails.Url)
                        && ICMSMethods.EnablePostActivity(activityDetails.OverallSummaryStatus, activityDetails.StatusCode, activityDetails.StartDate, activityDetails.EndDate, activityDetails.Url))
                    {
                        //update if objective achieved or not
                        learningobjectivestatusManager.UpdateLearningObjectiveStatus(activity.LearningObjectives.Select(lo => lo.LearningObjectiveStatus).ToList(), model.Stage);
                        learningobjectivestatusManager.SaveLearningObjectiveStatus();

                        //update answers of objective not achieved questions
                        learningobjectiveanswerManager.DeleteMany(activity.LearningObjectives.Where(lo => lo.LearningObjectiveStatus.IsObjectiveAchieved == (bool?)true).SelectMany(lo => lo.LearningObjectiveQuestions).Select(loq => { loq.LearningObjectiveAnswer.ActivityId = activity.ActivityId; return loq.LearningObjectiveAnswer; }).ToList());
                        learningobjectiveanswerManager.SaveLearningObjectiveAnswer();
                        learningobjectiveanswerManager.AddLearningObjectiveQuestionAnswers(activity.LearningObjectives.Where(lo => lo.LearningObjectiveStatus.IsObjectiveAchieved == (bool?)false).SelectMany(lo => lo.LearningObjectiveQuestions).Select(loq => { loq.LearningObjectiveAnswer.ActivityId = activity.ActivityId; return loq.LearningObjectiveAnswer; }).ToList());
                        learningobjectiveanswerManager.SaveLearningObjectiveAnswer();

                        questionManager.UpdateActivityQuestionAnswers(activity.ActivityQuestions.Select(aq => { aq.ActivityAnswer.ActivityId = activity.ActivityId; return aq.ActivityAnswer; }).ToList());
                        questionManager.SaveQuestions();

                        studentReflectionHeaderManager.UpdateActivityStatus(activity.ActivityId, studentId, model.Submit ? ActivityReflectionCodes.Completed : ActivityReflectionCodes.PostActivityDraft);
                        studentReflectionHeaderManager.SaveStudentReflectionHeader();
                        result = true;
                    }
                    break;
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, result));
        }
    }
}
