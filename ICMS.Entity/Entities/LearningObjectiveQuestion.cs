using System.ComponentModel.DataAnnotations.Schema;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Entity.Entities
{
    public class LearningObjectiveQuestion
    {
        public int LearningObjectiveQuestionId { get; set; }
        public int LearningObjectiveId { get; set; }
        public string ReflectionStatusCode { get; set; }
        public string Question { get; set; }
        public string QuestionTypeCode { get; set; }
        public int SequenceNo { get; set; }
        public string EFF_STATUS { get; set; }

        public bool IsActive
        {
            get { return EFF_STATUS == IsActiveValues.Active; }
            set { EFF_STATUS = value ? IsActiveValues.Active : IsActiveValues.Inactive; }
        }

        public LearningObjective LearningObjective { get; set; }
        public LearningObjectiveAnswer LearningObjectiveAnswer { get; set; }
    }
}
