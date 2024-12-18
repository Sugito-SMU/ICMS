using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    public class LearningObjectiveAnswerConfiguration : EntityTypeConfiguration<LearningObjectiveAnswer>
    {
        public LearningObjectiveAnswerConfiguration()
        {
            ToTable("PS_SIS_IC_SRLOA_VW");
            Property(g => g.LearningObjectiveQuestionId).HasColumnName("SIS_LOQ_ID").IsRequired();
            Property(g => g.LearningObjectiveId).HasColumnName("SIS_LO_ID").IsRequired();
            Property(g => g.ActivityId).HasColumnName("SIS_INTPART_ID").IsRequired().HasMaxLength(32);
            Property(g => g.StudentId).HasColumnName("EMPLID").IsRequired().HasMaxLength(11);
            Property(g => g.Answer).HasColumnName("SIS_LOQ_ANSWER");
            Property(g => g.CreatedBy).HasColumnName("CREATEOPRID").HasMaxLength(30);
            Property(g => g.ModifiedBy).HasColumnName("LAST_UPDT_OPRID").HasMaxLength(30);
            Property(g => g.CreatedDatetime).HasColumnName("CREATED_DTTM");
            Property(g => g.ModifiedDatetime).HasColumnName("LAST_UPDT_DTTM");

            HasKey(k => new { k.LearningObjectiveQuestionId, k.LearningObjectiveId, k.ActivityId, k.StudentId });
        }
    }
}