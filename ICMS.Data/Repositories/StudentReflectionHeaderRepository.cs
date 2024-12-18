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
    public class StudentReflectionHeaderRepository : RepositoryBase<StudentReflectionHeader>, IStudentReflectionHeaderRepository
    {
        public StudentReflectionHeaderRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public void UpdateActivityStatus(string activityId, string studentId, string activityStatus)
        {
            StudentReflectionHeader reflection = this.DbContext.StudentReflectionHeaders.FirstOrDefault(r => r.ActivityId == activityId && r.StudentId == studentId);

            if (reflection != null)
            {
                reflection.ActivityStatusCode = activityStatus;
                this.Update(reflection);
            }
            else
            {
                this.Add(new StudentReflectionHeader() { StudentId = studentId, ActivityId = activityId, ActivityStatusCode = activityStatus });
            }
        }
    }

    public interface IStudentReflectionHeaderRepository : IRepository<StudentReflectionHeader>
    {
        void UpdateActivityStatus(string activityId, string studentId, string activityStatus);
    }
}
