using ICMS.Data.Repositories;
using ICMS.Entity.Entities;

namespace ICMS.Manager
{
    // operations you want to expose
    public interface IAdminManager
    {
        Admin GetAdminByUsername(string userName);
        Admin GetAdminById(string studentId);
    }

    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepository adminRepository;

        public AdminManager(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public Admin GetAdminByUsername(string userName)
        {
            return adminRepository.GetAdminByUsername(userName);
        }

        public Admin GetAdminById(string studentId)
        {
            return adminRepository.GetById(studentId);
        }
    }
}
