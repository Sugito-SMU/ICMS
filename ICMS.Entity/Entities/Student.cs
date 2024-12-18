using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.Entities
{
    public class Student
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public string NtLoginId { get; set; }
        public string SmuEmail { get; set; }
        public string AdmitTerm { get; set; }

        public virtual List<Activity> Activities { get; set; }
    }
}
