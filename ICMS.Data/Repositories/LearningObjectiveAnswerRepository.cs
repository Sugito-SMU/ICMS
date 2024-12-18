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
    public class LearningObjectiveAnswerRepository : RepositoryBase<LearningObjectiveAnswer>, ILearningObjectiveAnswerRepository
    {
        public LearningObjectiveAnswerRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        public void DeleteMidAndPostAnswers(List<LearningObjectiveStatus> learningObjectiveStatuses)
        {
            learningObjectiveStatuses.ForEach(delegate (LearningObjectiveStatus los) {
                var remove = (from loa in this.DbContext.LearningObjectiveAnswers
                              join loq in this.DbContext.LearningObjectiveQuestions on new { loa.ActivityId, loa.StudentId, loa.LearningObjectiveId, loa.LearningObjectiveQuestionId } equals new { los.ActivityId, los.StudentId, los.LearningObjectiveId, loq.LearningObjectiveQuestionId }
                              where loq.EFF_STATUS == IsActiveValues.Active
                              select loa
                              );

                if (remove != null)
                {
                    this.DbContext.LearningObjectiveAnswers.RemoveRange(remove);
                }
            });
        }

        public void DeleteMany(List<LearningObjectiveAnswer> learningObjectiveAnswers)
        {
            learningObjectiveAnswers.ForEach(delegate (LearningObjectiveAnswer lo) {
                LearningObjectiveAnswer answer = this.DbContext.LearningObjectiveAnswers.FirstOrDefault(los => los.ActivityId == lo.ActivityId
                    && los.LearningObjectiveId == lo.LearningObjectiveId && los.LearningObjectiveQuestionId == lo.LearningObjectiveQuestionId && los.StudentId == lo.StudentId);

                if (answer != null)
                {
                    this.Delete(answer);
                }
            });
        }
        public void AddLearningObjectiveQuestionAnswers(List<LearningObjectiveAnswer> learningObjectiveAnswers)
        {
            List<LearningObjectiveAnswer> existingAnswers = learningObjectiveAnswers.Where(o => this.DbContext.LearningObjectiveAnswers.Any(n => o.ActivityId == n.ActivityId &&
                o.LearningObjectiveId == n.LearningObjectiveId && o.LearningObjectiveQuestionId == n.LearningObjectiveQuestionId && o.StudentId == n.StudentId)).ToList();

            learningObjectiveAnswers.ForEach(delegate (LearningObjectiveAnswer loq) {
                if(!existingAnswers.Any(ea => ea.ActivityId == loq.ActivityId && ea.LearningObjectiveId == loq.LearningObjectiveId
                    && ea.LearningObjectiveQuestionId == loq.LearningObjectiveQuestionId && ea.StudentId == loq.StudentId))
                {
                    this.Add(loq);
                }
                else
                {
                    this.DbContext.LearningObjectiveAnswers.Attach(loq);
                    this.DbContext.Entry(loq).Property(p => p.Answer).IsModified = true;
                }
            });
        }

        public void DeleteAnswers(string activityId, string studentId, string status)
        {
            var answers = (from answer in this.DbContext.LearningObjectiveAnswers
                        join question in this.DbContext.LearningObjectiveQuestions on new { answer.LearningObjectiveId, answer.LearningObjectiveQuestionId } equals new { question.LearningObjectiveId, question.LearningObjectiveQuestionId }
                        where (status == ActivityStageCode.PRE || (status == ActivityStageCode.MID && (question.ReflectionStatusCode == ActivityStageCode.MID || question.ReflectionStatusCode == ActivityStageCode.POST)) || question.ReflectionStatusCode == ActivityStageCode.POST) && 
                        answer.ActivityId == activityId && answer.StudentId == studentId
                        select answer).ToList();

            if(answers != null && answers.Count > 0)
            {
                this.DbContext.LearningObjectiveAnswers.RemoveRange(answers);
            }
        }
    }

    public interface ILearningObjectiveAnswerRepository : IRepository<LearningObjectiveAnswer>
    {
        void DeleteMidAndPostAnswers(List<LearningObjectiveStatus> learningObjectiveStatuses);
        void DeleteMany(List<LearningObjectiveAnswer> learningObjectiveAnswers);
        void AddLearningObjectiveQuestionAnswers(List<LearningObjectiveAnswer> learningObjectiveAnswers);
        void DeleteAnswers(string activityId, string studentId, string status);
    }
}
