using ICMS.Entity.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Entity.Entities
{
    public class LearningObjectiveStatus : CreatedModifiedBase
    {
        public int LearningObjectiveId { get; set; }
        public string ActivityId { get; set; }
        public string StudentId { get; set; }
        public string IS_LO_ON_TGT { get; set; } = "";
        public string IS_LO_ACHV { get; set; } = "";
        
        public bool? IsMidActivityOnTarget
        {
            get { return IS_LO_ON_TGT == YesNoValues.Yes ? true : IS_LO_ON_TGT == YesNoValues.No ? (bool?)false : null; }
            set { IS_LO_ON_TGT = value == null ? "" : (bool)value ? YesNoValues.Yes : YesNoValues.No; }
        }
        
        public bool? IsObjectiveAchieved
        {
            get { return IS_LO_ACHV == YesNoValues.Yes ? true : IS_LO_ACHV == YesNoValues.No ? (bool?)false : null; }
            set { IS_LO_ACHV = value == null ? "" : (bool)value ? YesNoValues.Yes : YesNoValues.No; }
        }

        public LearningObjective LearningObjective { get; set; }
        public Activity Activity { get; set; }
        public Student Student { get; set; }

        public LearningObjectiveStatus()
        {
            this.CreatedDatetime = DateTime.Now;
        }
    }
}
