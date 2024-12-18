using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.ViewModels
{
    public class ActivityUpdateViewModel
    {
        public string ActivityId { get; set; }
        public string Stage { get; set; }
        public List<LearningObjectiveViewModel> LearningObjectives { get; set; }
        public List<ActivityQuestionAnswerViewModel> ActivityQuestionAnswers { get; set; }
        public bool Submit { get; set; }
    }
}
