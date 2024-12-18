using ICMS.Common;
using ICMS.Data.Infrastructure;
using ICMS.Entity.Entities;
using System;
using System.Linq;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Data.Repositories
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Student GetStudentByUsername(string userName)
        {
            Student std1 = (Student)MCache.Get($"Student_{userName}");
            if (std1 == null)
            {
                var std2 = (from s in this.DbContext.Students
                            join sp in this.DbContext.StudentProgrammes on s.StudentId equals sp.StudentId
                            where s.NtLoginId == userName && sp.CareerCode == CareerCodes.UnderGraduate
                            && sp.ProgramOrder == 0 && Programs.Keys.Contains(sp.StatusCode)
                            select new { s, sp }).ToList();

                var std3 = std2.Where(s => (Convert.ToInt32(s.sp.AdmitTerm) >= AdmitTerm)).Take(1).ToList();

                std1 = std3.Count == 0 ? null : std3.ConvertAll(s => new Student()
                {
                    FullName = s.s.FullName,
                    NtLoginId = s.s.NtLoginId,
                    SmuEmail = s.s.SmuEmail,
                    StudentId = s.s.StudentId,
                    AdmitTerm = s.sp.AdmitTerm
                }).First();
                if (std1 != null)
                {
                    MCache.Set($"Student_{userName}", std1);
                }
            }
            return std1;
        }
    }

    public interface IStudentRepository : IRepository<Student>
    {
        Student GetStudentByUsername(string userName);
    }
}
