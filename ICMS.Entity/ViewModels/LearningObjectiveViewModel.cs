using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.ViewModels
{
    public class LearningObjectiveViewModel
    {
        public int LearningObjectiveId { get; set; }
        public string ActivityId { get; set; }
        public string StudentId { get; set; }
        public string Description { get; set; }
        public string MidActivityNotOnTargetInstruction { get; set; }
        public bool? IsSelected { get; set; }
        public bool? IsMidActivityOnTarget { get; set; }
        public bool? IsObjectiveAchieved { get; set; }

        public List<SequenceContentViewModel> LearningIndicators { get; set; }
        public List<LearningObjectiveQuestionAnswerViewModel> LearningObjectiveQuestions { get; set; }
        public List<LearningObjectiveQuestionAnswerViewModel> LearningObjectiveStageQuestionAnswers { get; set; }
    }
}