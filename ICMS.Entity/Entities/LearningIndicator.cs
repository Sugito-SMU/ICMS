using ICMS.Entity.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Entity.Entities
{
    public class LearningIndicator : CreatedModifiedBase
    {
        public int LearningIndicatorId { get; set; }
        public int LearningObjectiveId { get; set; }
        public string Description { get; set; }
        public int SequenceNo { get; set; }
        public string EFF_STATUS { get; set; }

        public bool IsActive
        {
            get { return EFF_STATUS == IsActiveValues.Active; }
            set { EFF_STATUS = value ? IsActiveValues.Active : IsActiveValues.Inactive; }
        }

        public LearningObjective LearningObjective { get; set; }
    }
}
