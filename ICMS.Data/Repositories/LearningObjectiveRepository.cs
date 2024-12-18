using ICMS.Data.Infrastructure;
using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ICMS.Common;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Data.Repositories
{
    public class LearningObjectiveRepository : RepositoryBase<LearningObjective>, ILearningObjectiveRepository
    {
        public LearningObjectiveRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public IEnumerable<LearningObjective> GetLearningObjectives(string studentId, string activityId, string stage)
        {
            IEnumerable<LearningObjective> learningobjectives;
            var activityType = this.DbContext.Activities.First(a => a.ActivityId == activityId).ActivityTypeCode;

            if (stage == ActivityStageCode.PRE)
            {
                var result = (from lo in this.DbContext.LearningObjectives
                              join los in this.DbContext.LearningObjectiveSets on lo.LearningObjectiveSetId equals los.LearningObjectiveSetId
                              join lostatus in this.DbContext.LearningObjectiveStatuses on new { lo.LearningObjectiveId, ActivityId = activityId, StudentId = studentId } equals new { lostatus.LearningObjectiveId, lostatus.ActivityId, lostatus.StudentId } into status
                              from lostatus in status.DefaultIfEmpty()
                              where los.ActivityType == activityType && lo.EFF_STATUS == IsActiveValues.Active 
                              && los.EFF_STATUS == IsActiveValues.Active
                              select new {
                                  LearningObjective = lo,
                                  LearningIndicator = lo.LearningIndicators.Where(li => li.EFF_STATUS == IsActiveValues.Active),
                                  LearningObjectiveQuestions = lo.LearningObjectiveQuestions.Where(loq => loq.ReflectionStatusCode == stage && loq.EFF_STATUS == IsActiveValues.Active),
                                  LearningObjectiveStatus = lostatus
                              });

                learningobjectives = result.ToArray().Select(o => { o.LearningObjective.IsSelected = o.LearningObjectiveStatus != null; return o.LearningObjective; });
            }
            else
            {
                var result = (from lo in this.DbContext.LearningObjectives
                              join los in this.DbContext.LearningObjectiveSets on lo.LearningObjectiveSetId equals los.LearningObjectiveSetId
                              join lostatus in this.DbContext.LearningObjectiveStatuses on new { lo.LearningObjectiveId, ActivityId = activityId, StudentId = studentId } equals new { lostatus.LearningObjectiveId, lostatus.ActivityId, lostatus.StudentId }
                              where los.ActivityType == activityType && lo.EFF_STATUS == IsActiveValues.Active
                              && los.EFF_STATUS == IsActiveValues.Active
                              select new
                              {
                                  LearningObjective = lo,
                                  LearningIndicator = lo.LearningIndicators.Where(li => li.EFF_STATUS == IsActiveValues.Active),
                                  LearningObjectiveQuestions = lo.LearningObjectiveQuestions.Where(loq => (loq.ReflectionStatusCode == stage || loq.ReflectionStatusCode == ActivityStageCode.PRE)
                                                                 && loq.EFF_STATUS == IsActiveValues.Active),
                                  LearningObjectiveStatus = lostatus
                              });

                learningobjectives = result.ToArray().Select(o => {
                    o.LearningObjective.IsMidActivityOnTarget = o.LearningObjectiveStatus.IsMidActivityOnTarget;
                    o.LearningObjective.IsObjectiveAchieved = o.LearningObjectiveStatus.IsObjectiveAchieved;
                    return o.LearningObjective;
                });
            }

            foreach (LearningObjective lo in learningobjectives)
            {
                lo.LearningObjectiveQuestions.ForEach(loq =>
                {
                    loq.LearningObjectiveAnswer = this.DbContext.LearningObjectiveAnswers.FirstOrDefault(loa => loa.LearningObjectiveId == lo.LearningObjectiveId
                        && loa.LearningObjectiveQuestionId == loq.LearningObjectiveQuestionId && loa.ActivityId == activityId && loa.StudentId == studentId);
                });

            }

            return learningobjectives;
        }
    }

    public interface ILearningObjectiveRepository : IRepository<LearningObjective>
    {
        IEnumerable<LearningObjective> GetLearningObjectives(string studentId, string activityId, string stage);
    }
}
