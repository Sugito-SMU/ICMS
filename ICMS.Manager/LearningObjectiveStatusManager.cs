using ICMS.Data.Infrastructure;
using ICMS.Data.Repositories;
using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Manager
{
    public class LearningObjectiveStatusManager : ILearningObjectiveStatusManager
    {
        private readonly ILearningObjectiveStatusRepository learningObjectiveStatusRepository;
        private readonly IUnitOfWork unitOfWork;

        public LearningObjectiveStatusManager(ILearningObjectiveStatusRepository learningObjectiveStatusRepository, IUnitOfWork unitOfWork)
        {
            this.learningObjectiveStatusRepository = learningObjectiveStatusRepository;
            this.unitOfWork = unitOfWork;
        }
        public void DeleteMany(List<LearningObjectiveStatus> learningObjectiveStatus)
        {
            learningObjectiveStatusRepository.DeleteMany(learningObjectiveStatus);
        }
        public void AddLearningObjectiveStatus(List<LearningObjectiveStatus> learningObjectiveStatus)
        {
            learningObjectiveStatusRepository.AddLearningObjectiveStatus(learningObjectiveStatus);
        }
        public void UpdateLearningObjectiveStatus(List<LearningObjectiveStatus> learningObjectiveStatuses, string activityStage)
        {
            learningObjectiveStatusRepository.UpdateLearningObjectiveStatus(learningObjectiveStatuses, activityStage);
        }
        public void ResetLearningObjectiveStatus(string activityId, string studentId, string status)
        {
            if (status == ActivityReflectionCodes.PreActivityDraft)
            {
                learningObjectiveStatusRepository.Delete(s => s.ActivityId == activityId && s.StudentId == studentId);
            }
            else
            {
                learningObjectiveStatusRepository.ResetLearningObjectiveStatus(activityId, studentId, status);
            }
        }
        public void SaveLearningObjectiveStatus()
        {
            unitOfWork.Commit();
        }
    }
    // operations you want to expose
    public interface ILearningObjectiveStatusManager
    {
        void DeleteMany(List<LearningObjectiveStatus> learningObjectiveStatus);
        void AddLearningObjectiveStatus(List<LearningObjectiveStatus> learningObjectiveStatus);
        void UpdateLearningObjectiveStatus(List<LearningObjectiveStatus> learningObjectiveStatuses, string activityStage);
        void ResetLearningObjectiveStatus(string activityId, string studentId, string status);
        void SaveLearningObjectiveStatus();
    }
}
