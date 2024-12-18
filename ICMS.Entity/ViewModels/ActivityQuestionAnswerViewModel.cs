using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.ViewModels
{
    public class ActivityQuestionAnswerViewModel
    {
        public int ActivityQuestionId { get; set; }
        public string ActivityId { get; set; }
        public string StudentId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string QuestionGuide { get; set; }
    }
}
