using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.ViewModels
{
    public class StudentViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string AdmitTerm { get; set; }
        public bool IsAdmin { get; set; }
    }
}
