using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    public class LearningObjectiveQuestionConfiguration : EntityTypeConfiguration<LearningObjectiveQuestion>
    {
        public LearningObjectiveQuestionConfiguration()
        {
            ToTable("PS_SIS_IC_LOQ_VW");
            Property(g => g.LearningObjectiveQuestionId).HasColumnName("SIS_LOQ_ID").IsRequired();
            Property(g => g.LearningObjectiveId).HasColumnName("SIS_LO_ID");
            Property(g => g.Question).HasColumnName("SIS_LOQ_TEXT");
            Property(g => g.QuestionTypeCode).HasColumnName("SIS_INPUT_TYPE").HasMaxLength(2);
            Property(g => g.ReflectionStatusCode).HasColumnName("SIS_R_STATUS").HasMaxLength(2);
            Property(g => g.SequenceNo).HasColumnName("SEQNUM");
            Property(g => g.EFF_STATUS).HasColumnName("EFF_STATUS").HasMaxLength(1);
            Ignore(g => g.IsActive);
            Ignore(g => g.LearningObjectiveAnswer);

            HasKey(k => new { k.LearningObjectiveQuestionId });

            //HasOptional(q => q.LearningObjectiveAnswer).WithRequired(a => a.LearningObjectiveQuestion);
        }
    }
}