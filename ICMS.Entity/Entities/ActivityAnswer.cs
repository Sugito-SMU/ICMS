using ICMS.Entity.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.Entities
{
    public class ActivityAnswer : CreatedModifiedBase
    {
        public int ActivityQuestionId { get; set; }
        public string ActivityId { get; set; }
        public string StudentId { get; set; }
        public string Answer { get; set; }

        public ActivityQuestion ActivityQuestion { get; set; }
        public Activity Activity { get; set; }
        public Student Student { get; set; }

        public ActivityAnswer()
        {
            this.CreatedDatetime = DateTime.Now;
        }
    }
}
