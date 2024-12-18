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
    public interface IStudentReflectionHeaderManager
    {
        void UpdateActivityStatus(string activityId, string studentId, string activityStatus);
        void SaveStudentReflectionHeader();
    }

    public class StudentReflectionHeaderManager : IStudentReflectionHeaderManager
    {
        private readonly IStudentReflectionHeaderRepository studentReflectionHeaderRepository;
        private readonly IUnitOfWork unitOfWork;

        public StudentReflectionHeaderManager(IStudentReflectionHeaderRepository studentReflectionHeaderRepository, IUnitOfWork unitOfWork)
        {
            this.studentReflectionHeaderRepository = studentReflectionHeaderRepository;
            this.unitOfWork = unitOfWork;
        }

        public void UpdateActivityStatus(string activityId, string studentId, string activityStatus)
        {
            studentReflectionHeaderRepository.UpdateActivityStatus(activityId, studentId, activityStatus);
        }

        public void SaveStudentReflectionHeader()
        {
            unitOfWork.Commit();
        }
    }
}
