using ICMS.Common;
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
    public interface ILearningObjectiveManager
    {
        IEnumerable<LearningObjective> GetLearningObjectives(string studentId, string activityId, string stage);
        void SaveLearningObjective();
    }

    public class LearningObjectiveManager : ILearningObjectiveManager
    {
        private readonly ILearningObjectiveRepository learningObjectiveRepository;
        private readonly IUnitOfWork unitOfWork;

        public LearningObjectiveManager(ILearningObjectiveRepository learningObjectiveRepository, IUnitOfWork unitOfWork)
        {
            this.learningObjectiveRepository = learningObjectiveRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<LearningObjective> GetLearningObjectives(string studentId, string activityId, string stage)
        {
            return learningObjectiveRepository.GetLearningObjectives(studentId, activityId, stage);
        }

        public void SaveLearningObjective()
        {
            unitOfWork.Commit();
        }
    }
}
