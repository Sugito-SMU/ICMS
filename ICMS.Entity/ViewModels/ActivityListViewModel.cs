using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.ViewModels
{
    public class ActivityListViewModel
    {
        public string ActivityId { get; set; }
        public string PositionTitle { get; set; }
        public bool ProBono { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ReflectionStatusDescription { get; set; }
        public string Url { get; set; }

    }
}
