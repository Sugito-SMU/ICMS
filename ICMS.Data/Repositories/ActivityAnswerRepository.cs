using ICMS.Data.Infrastructure;
using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Repositories
{
    public class ActivityAnswerRepository : RepositoryBase<ActivityAnswer>, IActivityAnswerRepository
    {
        public ActivityAnswerRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        public void UpdateActivityQuestionAnswers(List<ActivityAnswer> activityAnswers)
        {
            List<ActivityAnswer> existingAnswers = activityAnswers.Where(o => this.DbContext.ActivityAnswers.Any(n => o.ActivityId == n.ActivityId &&
                o.ActivityQuestionId == n.ActivityQuestionId && o.StudentId == n.StudentId)).ToList();

            activityAnswers.ForEach(delegate (ActivityAnswer aq) {
                if (!existingAnswers.Any(ea => ea.ActivityId == aq.ActivityId && ea.ActivityQuestionId == aq.ActivityQuestionId && ea.StudentId == aq.StudentId))
                {
                    this.Add(aq);
                }
                else
                {
                    this.DbContext.ActivityAnswers.Attach(aq);
                    this.DbContext.Entry(aq).Property(p => p.Answer).IsModified = true;
                }
            });
        }
    }

    public interface IActivityAnswerRepository : IRepository<ActivityAnswer>
    {
        void UpdateActivityQuestionAnswers(List<ActivityAnswer> activityAnswers);
    }
}
