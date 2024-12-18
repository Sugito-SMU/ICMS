using ICMS.Common;
using ICMS.Data.Infrastructure;
using ICMS.Entity.Entities;
using System;
using System.Linq;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Data.Repositories
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Admin GetAdminByUsername(string userName)
        {
            Admin adm1 = (Admin)MCache.Get($"Admin_{userName}");
            if (adm1 == null)
            {
                var adm2 = (from a in this.DbContext.Admins
                             where a.Username == userName
                             select a).Take(1).ToList();

                adm1 = adm2.Count == 0 ? null : adm2.ConvertAll(a => new Admin()
                {
                    Username = a.Username
                }).First();

                if (adm1 != null)
                {
                    MCache.Set($"Admin_{userName}", adm1);
                }
            }
            return adm1;
        }
    }

    public interface IAdminRepository : IRepository<Admin>
    {
        Admin GetAdminByUsername(string userName);
    }
}
