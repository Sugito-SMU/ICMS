using ICMS.Entity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ICMS.Data.Configuration
{
    public class ActivityAnswerConfiguration : EntityTypeConfiguration<ActivityAnswer>
    {
        public ActivityAnswerConfiguration()
        {
            ToTable("PS_SIS_IC_STDRA_VW");
            Property(g => g.StudentId).HasColumnName("EMPLID").IsRequired().HasMaxLength(11);
            Property(g => g.ActivityId).HasColumnName("SIS_INTPART_ID").IsRequired().HasMaxLength(32);
            Property(g => g.ActivityQuestionId).HasColumnName("SIS_RQ_ID").IsRequired();
            Property(g => g.Answer).HasColumnName("SIS_RQ_ANSWER");
            Property(g => g.CreatedBy).HasColumnName("CREATEOPRID").HasMaxLength(30);
            Property(g => g.ModifiedBy).HasColumnName("LAST_UPDT_OPRID").HasMaxLength(30);
            Property(g => g.CreatedDatetime).HasColumnName("CREATED_DTTM");
            Property(g => g.ModifiedDatetime).HasColumnName("LAST_UPDT_DTTM");

            HasKey(k => new { k.ActivityQuestionId, k.ActivityId, k.StudentId });
        }
    }
}