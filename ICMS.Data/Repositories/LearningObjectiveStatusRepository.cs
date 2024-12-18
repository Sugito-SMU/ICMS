using ICMS.Data.Infrastructure;
using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Data.Repositories
{
    public class LearningObjectiveStatusRepository : RepositoryBase<LearningObjectiveStatus>, ILearningObjectiveStatusRepository
    {
        public LearningObjectiveStatusRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public void DeleteMany(List<LearningObjectiveStatus> learningObjectiveStatuses)
        {
            learningObjectiveStatuses.ForEach(delegate (LearningObjectiveStatus lo) {
                LearningObjectiveStatus status = this.DbContext.LearningObjectiveStatuses.FirstOrDefault(los => los.ActivityId == lo.ActivityId
                    && los.LearningObjectiveId == lo.LearningObjectiveId && los.StudentId == lo.StudentId);

                if (status != null)
                {
                    this.Delete(status);
                }
            });
        }

        public void AddLearningObjectiveStatus(List<LearningObjectiveStatus> learningObjectiveStatus)
        {
            List<LearningObjectiveStatus> existingAnswers = learningObjectiveStatus.Where(o => this.DbContext.LearningObjectiveStatuses.Any(n => o.ActivityId == n.ActivityId &&
                o.LearningObjectiveId == n.LearningObjectiveId && o.StudentId == n.StudentId)).ToList();

            learningObjectiveStatus.ForEach(delegate (LearningObjectiveStatus loq) {
                if (!existingAnswers.Any(ea => ea.ActivityId == loq.ActivityId && ea.LearningObjectiveId == loq.LearningObjectiveId
                     && ea.StudentId == loq.StudentId))
                {
                    this.Add(loq);
                }
            });
        }

        public void UpdateLearningObjectiveStatus(List<LearningObjectiveStatus> learningObjectiveStatuses, string activityStage)
        {
            learningObjectiveStatuses.ForEach(delegate (LearningObjectiveStatus n) {
                LearningObjectiveStatus status = this.DbContext.LearningObjectiveStatuses.Single(c => c.ActivityId == n.ActivityId
                    && c.LearningObjectiveId == n.LearningObjectiveId && c.StudentId == n.StudentId);
                if (status != null)
                {
                    if (activityStage == ActivityStageCode.MID)
                    {
                        status.IsMidActivityOnTarget = n.IsMidActivityOnTarget;
                        this.Update(status);
                    }
                    else
                    {
                        status.IsObjectiveAchieved = n.IsObjectiveAchieved;
                        this.Update(status);
                    }
                }
            });
        }

        public void ResetLearningObjectiveStatus(string activityId, string studentId, string status)
        {
            var statuses = this.DbContext.LearningObjectiveStatuses.Where(s => s.ActivityId == activityId && s.StudentId == studentId).ToList();
            statuses.ForEach(s => { s.IsObjectiveAchieved = null; s.IsMidActivityOnTarget = (status == ActivityReflectionCodes.MidActivityDraft ? null : s.IsMidActivityOnTarget); });
        }
    }

    public interface ILearningObjectiveStatusRepository : IRepository<LearningObjectiveStatus>
    {
        void DeleteMany(List<LearningObjectiveStatus> learningObjectiveStatuses);
        void AddLearningObjectiveStatus(List<LearningObjectiveStatus> learningObjectiveStatus);
        void UpdateLearningObjectiveStatus(List<LearningObjectiveStatus> learningObjectiveStatuses, string activityStage);
        void ResetLearningObjectiveStatus(string activityId, string studentId, string status);
    }
}
