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
    public class ActivityQuestion : CreatedModifiedBase
    {
        public int ActivityQuestionId { get; set; }
        public int ActivityQuestionSetId { get; set; }
        public string Question { get; set; }
        public string QuestionGuide { get; set; }
        public int SequenceNo { get; set; }
        public string EFF_STATUS { get; set; }

        public bool IsActive
        {
            get { return EFF_STATUS == IsActiveValues.Active; }
            set { EFF_STATUS = value ? IsActiveValues.Active : IsActiveValues.Inactive; }
        }

        public ActivityAnswer ActivityAnswer { get; set; }
    }
}
