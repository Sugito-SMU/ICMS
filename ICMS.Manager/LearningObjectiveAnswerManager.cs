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
    public interface ILearningObjectiveAnswerManager
    {
        void DeleteMidAndPostAnswers(List<LearningObjectiveStatus> learningObjectiveStatuses);
        void DeleteMany(List<LearningObjectiveAnswer> learningObjectiveAnswers);
        void AddLearningObjectiveQuestionAnswers(List<LearningObjectiveAnswer> learningObjectiveAnswers);
        void DeleteAnswers(string activityId, string studentId, string status);
        void SaveLearningObjectiveAnswer();
    }

    public class LearningObjectiveAnswerManager : ILearningObjectiveAnswerManager
    {
        private readonly ILearningObjectiveAnswerRepository learningObjectiveAnswerRepository;
        private readonly IUnitOfWork unitOfWork;

        public LearningObjectiveAnswerManager(ILearningObjectiveAnswerRepository learningObjectiveAnswerRepository, IUnitOfWork unitOfWork)
        {
            this.learningObjectiveAnswerRepository = learningObjectiveAnswerRepository;
            this.unitOfWork = unitOfWork;
        }
        public void DeleteMidAndPostAnswers(List<LearningObjectiveStatus> learningObjectiveStatuses)
        {
            learningObjectiveAnswerRepository.DeleteMidAndPostAnswers(learningObjectiveStatuses);
        }
        public void DeleteMany(List<LearningObjectiveAnswer> learningObjectiveAnswers)
        {
            learningObjectiveAnswerRepository.DeleteMany(learningObjectiveAnswers);
        }
        public void AddLearningObjectiveQuestionAnswers(List<LearningObjectiveAnswer> learningObjectiveAnswers)
        {
            learningObjectiveAnswerRepository.AddLearningObjectiveQuestionAnswers(learningObjectiveAnswers);
        }

        public void DeleteAnswers(string activityId, string studentId, string status)
        {
            learningObjectiveAnswerRepository.DeleteAnswers(activityId, studentId, status);
        }

        public void SaveLearningObjectiveAnswer()
        {
            unitOfWork.Commit();
        }
    }
}
