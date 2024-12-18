using ICMS.Common;
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
    public class ActivityQuestionRepository : RepositoryBase<ActivityQuestion>, IActivityQuestionRepository
    {
        public ActivityQuestionRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        public IEnumerable<ActivityQuestion> GetActivityQuestions(string studentId, string activityId)
        {
            var activityType = this.DbContext.Activities.First(a => a.ActivityId == activityId).ActivityTypeCode;

            var result = (from aq in this.DbContext.ActivityQuestions
                          join rs in this.DbContext.ReflectionSets on aq.ActivityQuestionSetId equals rs.ReflectionSetId
                          join aa in this.DbContext.ActivityAnswers on new { aq.ActivityQuestionId, ActivityId = activityId, StudentId = studentId } equals new { aa.ActivityQuestionId, aa.ActivityId, aa.StudentId } into answers
                          from aa in answers.DefaultIfEmpty()
                          where rs.ActivityTypeCode == activityType && aq.EFF_STATUS == IsActiveValues.Active
                          select new { aq, aa }
                          );

            return result.ToArray().Select(r => { r.aq.ActivityAnswer = r.aa; return r.aq; });
        }
    }

    public interface IActivityQuestionRepository : IRepository<ActivityQuestion>
    {
        IEnumerable<ActivityQuestion> GetActivityQuestions(string studentId, string activityId);
    }
}
