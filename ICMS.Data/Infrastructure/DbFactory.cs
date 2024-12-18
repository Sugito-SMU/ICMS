using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        ICMSEntities dbContext;

        public ICMSEntities Init()
        {
            return dbContext ?? (dbContext = new ICMSEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
