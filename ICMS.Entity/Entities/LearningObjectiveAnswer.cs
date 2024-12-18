using ICMS.Entity.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICMS.Entity.Entities
{
    public class LearningObjectiveAnswer : CreatedModifiedBase
    {
        public int LearningObjectiveQuestionId { get; set; }
        public int LearningObjectiveId { get; set; }
        public string ActivityId { get; set; }
        public string StudentId { get; set; }
        public string Answer { get; set; }

        public LearningObjectiveQuestion LearningObjectiveQuestion { get; set; }
        public LearningObjective LearningObjective { get; set; }
        public Activity Activity { get; set; }
        public Student Student { get; set; }

        public LearningObjectiveAnswer()
        {
            this.CreatedDatetime = DateTime.Now;
        }
    }
}
