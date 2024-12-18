using ICMS.Entity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ICMS.Data.Configuration
{
    public class ActivityQuestionConfiguration : EntityTypeConfiguration<ActivityQuestion>
    {
        public ActivityQuestionConfiguration()
        {
            ToTable("PS_SIS_IC_RQ_VW");
            Property(g => g.ActivityQuestionId).HasColumnName("SIS_RQ_ID").IsRequired();
            Property(g => g.ActivityQuestionSetId).HasColumnName("SIS_RS_ID").IsRequired();
            Property(g => g.Question).HasColumnName("SIS_RQ_TEXT");
            Property(g => g.QuestionGuide).HasColumnName("SIS_RQ_GUIDE");
            Property(g => g.SequenceNo).HasColumnName("SEQNUM");
            Property(g => g.CreatedBy).HasColumnName("CREATEOPRID").HasMaxLength(30);
            Property(g => g.ModifiedBy).HasColumnName("LAST_UPDT_OPRID").HasMaxLength(30);
            Property(g => g.CreatedDatetime).HasColumnName("CREATED_DTTM");
            Property(g => g.ModifiedDatetime).HasColumnName("LAST_UPDT_DTTM");
            Property(g => g.EFF_STATUS).HasColumnName("EFF_STATUS").HasMaxLength(1);
            Ignore(g => g.IsActive);
            Ignore(g => g.ActivityAnswer);

            HasKey(k => new { k.ActivityQuestionId });

            //HasOptional(q => q.ActivityAnswer).WithRequired(a => a.ActivityQuestion);
        }
    }
}