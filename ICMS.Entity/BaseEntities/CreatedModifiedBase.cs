using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.BaseEntities
{
    public class CreatedModifiedBase
    {
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDatetime { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDatetime { get; set; }
    }
}
