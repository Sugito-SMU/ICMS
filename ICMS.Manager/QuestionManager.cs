using ICMS.Data.Infrastructure;
using ICMS.Data.Repositories;
using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Manager
{
    // operations you want to expose
    public interface IQuestionManager
    {
        IEnumerable<ActivityQuestion> GetActivityQuestions(string studentId, string activityId);
        void UpdateActivityQuestionAnswers(List<ActivityAnswer> activityAnswers);
        void DeleteAnswers(string activityId, string studentId);
        void SaveQuestions();
    }

    public class QuestionManager : IQuestionManager
    {
        private readonly IActivityQuestionRepository questionRepository;
        private readonly IActivityAnswerRepository answerRepository;
        private readonly IUnitOfWork unitOfWork;

        public QuestionManager(IActivityQuestionRepository questionRepository, IActivityAnswerRepository answerRepository, IUnitOfWork unitOfWork)
        {
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<ActivityQuestion> GetActivityQuestions(string studentId, string activityId)
        {
            //return questionRepository.GetMany(q => q.StudentId == studentId && q.ActivityId == activityId).OrderBy(q => q.SequenceNo);
            return questionRepository.GetActivityQuestions(studentId, activityId);
        }

        public void UpdateActivityQuestionAnswers(List<ActivityAnswer> activityAnswers)
        {
            answerRepository.UpdateActivityQuestionAnswers(activityAnswers);
        }

        public void DeleteAnswers(string activityId, string studentId)
        {
            answerRepository.Delete(a => a.ActivityId == activityId && a.StudentId == studentId);
        }

        public void SaveQuestions()
        {
            unitOfWork.Commit();
        }
    }
}
