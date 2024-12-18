using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Entity.Entities
{
    public class LearningObjective
    {
        public int LearningObjectiveId { get; set; }
        public int LearningObjectiveSetId { get; set; }
        public string Description { get; set; }
        public string MidActivityNotOnTargetInstruction { get; set; }
        public bool? IsMidActivityOnTarget { get; set; }
        public bool? IsObjectiveAchieved { get; set; }
        public bool IsSelected { get; set; }
        public string EFF_STATUS { get; set; }

        public bool IsActive {
            get { return EFF_STATUS == IsActiveValues.Active; }
            set { EFF_STATUS = value ? IsActiveValues.Active : IsActiveValues.Inactive; }
        }

        public virtual List<LearningIndicator> LearningIndicators { get; set; }
        public List<LearningObjectiveQuestion> LearningObjectiveQuestions { get; set; }
        public LearningObjectiveStatus LearningObjectiveStatus { get; set; }
    }
}
