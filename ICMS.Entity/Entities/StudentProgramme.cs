using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.Entities
{
    public class StudentProgramme
    {
        public string StudentId { get; set; }
        public string CareerCode { get; set; }
        public Int16 ProgramOrder { get; set; }
        public string ProgrammeCode { get; set; }
        public string Description { get; set; }
        public string StatusCode { get; set; }
        public string AdmitTerm { get; set; }
    }
}
