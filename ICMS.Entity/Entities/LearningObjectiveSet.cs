using ICMS.Entity.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Entity.Entities
{
    public class LearningObjectiveSet : CreatedModifiedBase
    {
        public int LearningObjectiveSetId { get; set; }
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public string EFF_STATUS { get; set; }

        public bool IsActive
        {
            get { return EFF_STATUS == IsActiveValues.Active; }
            set { EFF_STATUS = value ? IsActiveValues.Active : IsActiveValues.Inactive; }
        }
    }
}
