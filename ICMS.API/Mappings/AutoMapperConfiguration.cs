using AutoMapper;
using ICMS.Common;
using ICMS.Entity.Entities;
using ICMS.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ICMS.Common.ICMSConstants;

namespace ICMS.API.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //domain model to view model mapping
                cfg.CreateMap<Activity, ActivityListViewModel>()
                    .ForMember(vm => vm.ProBono, map => map.MapFrom(dm => dm.ProBonoRecognised > 0))
                    .ForMember(vm => vm.StartDate, map => map.MapFrom(dm => dm.StartDate.ToString(Constants.DisplayDateFormat)))
                    .ForMember(vm => vm.EndDate, map => map.MapFrom(dm => dm.EndDate.ToString(Constants.DisplayDateFormat)))
                    .ForMember(vm => vm.ReflectionStatusDescription, map => map.MapFrom(dm => dm.StatusDescription))
                    .ForMember(vm => vm.Url, map => map.MapFrom(dm => dm.Url));

                cfg.CreateMap<Activity, ActivityViewModel>()
                    .ForMember(vm => vm.StartDate, map => map.MapFrom(dm => dm.StartDate.ToString(Constants.DisplayDateFormat)))
                    .ForMember(vm => vm.EndDate, map => map.MapFrom(dm => dm.EndDate.ToString(Constants.DisplayDateFormat)))
                    .ForMember(vm => vm.Duration, map => map.MapFrom(dm => Convert.ToInt32(dm.Duration)))
                    .ForMember(vm => vm.UnitRecognised, map => map.MapFrom(dm => Convert.ToInt32(dm.UnitRecognised)))
                    .ForMember(vm => vm.UnitsRecognised, map => map.MapFrom(dm => dm.UnitsRecognised))
                    .ForMember(vm => vm.ReflectionStatusCode, map => map.MapFrom(dm => dm.StatusCode))
                    .ForMember(vm => vm.ReflectionStatusDescription, map => map.MapFrom(dm => dm.StatusDescription))
                    .ForMember(vm => vm.CountryDescription, map => map.MapFrom(dm => dm.CountryName))
                    .ForMember(vm => vm.OrganisationDescription, map => map.MapFrom(dm => dm.OrganisationName))
                    .ForMember(vm => vm.Unit, map => map.MapFrom(dm => UnitTypes[dm.Unit]))
                    .ForMember(vm => vm.Url, map => map.MapFrom(dm => dm.Url))
                    .ForMember(vm => vm.ShowPreActivity, map => map.MapFrom(dm => ICMSMethods.ShowPreActivity(dm.OverallSummaryStatus, dm.StatusCode, dm.StartDate, dm.Url)))
                    .ForMember(vm => vm.ShowMidActivity, map => map.MapFrom(dm => ICMSMethods.ShowMidActivity(dm.OverallSummaryStatus, dm.StatusCode, dm.StartDate, dm.EndDate, dm.Url)))
                    .ForMember(vm => vm.ShowPostActivity, map => map.MapFrom(dm => ICMSMethods.ShowPostActivity(dm.OverallSummaryStatus, dm.StatusCode, dm.StartDate, dm.EndDate, dm.Url)))
                    .ForMember(vm => vm.EnablePreActivity, map => map.MapFrom(dm => ICMSMethods.EnablePreActivity(dm.OverallSummaryStatus, dm.StatusCode, dm.StartDate, dm.ProBonoRecognised > 0, dm.Url)))
                    .ForMember(vm => vm.EnableMidActivity, map => map.MapFrom(dm => ICMSMethods.EnableMidActivity(dm.OverallSummaryStatus, dm.StatusCode, dm.StartDate, dm.EndDate, dm.Url)))
                    .ForMember(vm => vm.EnablePostActivity, map => map.MapFrom(dm => ICMSMethods.EnablePostActivity(dm.OverallSummaryStatus, dm.StatusCode, dm.StartDate, dm.EndDate, dm.Url)))
                    .ForMember(vm => vm.OverallGradeDescription, map => map.MapFrom(dm => dm.OverallGradeDescription ?? Constants.NullFiller));

                cfg.CreateMap<LearningObjective, LearningObjectiveViewModel>()
                    .ForMember(vm => vm.LearningIndicators, map => map.MapFrom(dm => dm.LearningIndicators.OrderBy(li => li.SequenceNo)))
                    .ForMember(vm => vm.LearningObjectiveQuestions, map => map.MapFrom(dm => dm.LearningObjectiveQuestions.Where(loq => loq.ReflectionStatusCode == ActivityStageCode.PRE).OrderBy(loq => loq.SequenceNo)))
                    .ForMember(vm => vm.LearningObjectiveStageQuestionAnswers, map => map.MapFrom(dm => dm.LearningObjectiveQuestions.Where(loq => loq.ReflectionStatusCode != ActivityStageCode.PRE).OrderBy(loq => loq.SequenceNo)))
                    .ForMember(vm => vm.IsMidActivityOnTarget, map => map.MapFrom(dm => dm.IsMidActivityOnTarget))
                    .ForMember(vm => vm.IsObjectiveAchieved, map => map.MapFrom(dm => dm.IsObjectiveAchieved));

                cfg.CreateMap<LearningIndicator, SequenceContentViewModel>()
                    .ForMember(vm => vm.Content, map => map.MapFrom(dm => dm.Description));

                cfg.CreateMap<LearningObjectiveQuestion, LearningObjectiveQuestionAnswerViewModel>()
                    .ForMember(vm => vm.Answer, map => map.MapFrom(dm => dm.LearningObjectiveAnswer.Answer));

                cfg.CreateMap<ActivityQuestion, ActivityQuestionAnswerViewModel>()
                    .ForMember(vm => vm.Answer, map => map.MapFrom(dm => dm.ActivityAnswer.Answer));

                cfg.CreateMap<OverallSummary, OverallSummaryViewModel>()
                    .ForMember(vm => vm.UnitsRecognized, map => map.MapFrom(dm => dm.UnitRecognised))
                    .ForMember(vm => vm.UnitsRequired, map => map.MapFrom(dm => dm.UnitRequired))
                    .ForMember(vm => vm.UnitsRemaining, map => map.MapFrom(dm => dm.UnitRemaining))
                    .ForMember(vm => vm.Status, map => map.MapFrom(dm => OverallStatusForStudents[dm.ActivityTypeCode][dm.OverallStatusCode]))
                    .ForMember(vm => vm.Unit, map => map.MapFrom(dm => UnitTypes[dm.UnitType]))
                    .ForMember(vm => vm.ProBono, map => map.MapFrom(dm => dm.ProBono));

                cfg.CreateMap<Student, StudentViewModel>()
                    .ForMember(vm => vm.Name, map => map.MapFrom(dm => dm.FullName))
                    .ForMember(vm => vm.UserId, map => map.MapFrom(dm => dm.StudentId))
                    .ForMember(vm => vm.AdmitTerm, map => map.MapFrom(dm => dm.AdmitTerm))
                    .ForMember(vm => vm.IsAdmin, map => map.MapFrom(dm => false));

                cfg.CreateMap<Admin, StudentViewModel>()
                    .ForMember(vm => vm.Name, map => map.MapFrom(dm => dm.Username))
                    .ForMember(vm => vm.UserId, map => map.MapFrom(dm => dm.Username))
                    .ForMember(vm => vm.IsAdmin, map => map.MapFrom(dm => true));

                cfg.CreateMap<KeyValueDescription, ActivityStatusViewModel>()
                    .ForMember(vm => vm.Description, map => map.MapFrom(dm => dm.LongName))
                    .ForMember(vm => vm.Code, map => map.MapFrom(dm => dm.FieldValue));

                //view model to domain model mapping
                cfg.CreateMap<ActivityUpdateViewModel, Activity>(MemberList.Source)
                    .ForMember(dm => dm.ActivityQuestions, map => map.MapFrom(vm => vm.ActivityQuestionAnswers));

                cfg.CreateMap<LearningObjectiveViewModel, LearningObjective>(MemberList.Source)
                    .ForMember(dm => dm.LearningObjectiveStatus, map => map.MapFrom(vm => (vm.IsSelected != null && !Convert.ToBoolean(vm.IsSelected)) ? null : new LearningObjectiveStatus()
                    {
                        ActivityId = vm.ActivityId,
                        IsMidActivityOnTarget = vm.IsMidActivityOnTarget,
                        IsObjectiveAchieved = vm.IsObjectiveAchieved,
                        LearningObjectiveId = vm.LearningObjectiveId,
                        StudentId = GlobalVariables.StudentId
                    }))
                    .ForMember(dm => dm.LearningObjectiveQuestions, map => map.MapFrom(vm => (vm.IsSelected == null ? vm.LearningObjectiveStageQuestionAnswers : vm.LearningObjectiveQuestions)));

                cfg.CreateMap<ActivityQuestionAnswerViewModel, ActivityQuestion>(MemberList.Source)
                    .ForMember(dm => dm.ActivityAnswer, map => map.MapFrom(vm => new ActivityAnswer()
                    {
                        ActivityId = vm.ActivityId,
                        Answer = vm.Answer,
                        ActivityQuestionId = vm.ActivityQuestionId,
                        StudentId = GlobalVariables.StudentId
                    }));

                cfg.CreateMap<LearningObjectiveQuestionAnswerViewModel, LearningObjectiveQuestion>(MemberList.Source)
                    .ForMember(dm => dm.LearningObjectiveAnswer, map => map.MapFrom(vm => new LearningObjectiveAnswer()
                     {
                         ActivityId = vm.ActivityId,
                         Answer = vm.Answer,
                         LearningObjectiveId = vm.LearningObjectiveId,
                         LearningObjectiveQuestionId = vm.LearningObjectiveQuestionId,
                         StudentId = GlobalVariables.StudentId
                    }));
            });
        }
    }
}