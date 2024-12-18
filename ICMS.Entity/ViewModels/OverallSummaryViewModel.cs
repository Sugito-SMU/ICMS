using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.ViewModels
{
    public class OverallSummaryViewModel
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public decimal UnitsRequired { get; set; }
        public decimal UnitsRecognized { get; set; }
        public string Unit { get; set; }
        public decimal ProBono { get; set; }
        public decimal UnitsRemaining { get; set; }
        public string ProgrammeCode { get; set; }
    }
}
