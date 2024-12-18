using ICMS.Data.Repositories;
using ICMS.Entity.Entities;

namespace ICMS.Manager
{
    // operations you want to expose
    public interface IStudentManager
    {
        Student GetStudentByUsername(string userName);
        Student GetStudentById(string studentId);
    }

    public class StudentManager : IStudentManager
    {
        private readonly IStudentRepository studentRepository;

        public StudentManager(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public Student GetStudentByUsername(string userName)
        {
            return studentRepository.GetStudentByUsername(userName);
        }

        public Student GetStudentById(string studentId)
        {
            return studentRepository.Get(s => s.StudentId == studentId);
        }
    }
}
