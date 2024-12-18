using ICMS.Entity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ICMS.Data.Configuration
{
    class LearningObjectiveSetConfiguration : EntityTypeConfiguration<LearningObjectiveSet>
    {
        public LearningObjectiveSetConfiguration()
        {
            ToTable("PS_SIS_IC_LOS_VW");
            Property(g => g.LearningObjectiveSetId).HasColumnName("SIS_LOS_ID").IsRequired();
            Property(g => g.ActivityType).HasColumnName("SIS_INTERN_TYPE").HasMaxLength(2);
            Property(g => g.Description).HasColumnName("SIS_LO_TEXT").HasMaxLength(50);
            Property(g => g.EFF_STATUS).HasColumnName("EFF_STATUS").HasMaxLength(1);
            Property(g => g.CreatedBy).HasColumnName("CREATEOPRID").HasMaxLength(30);
            Property(g => g.ModifiedBy).HasColumnName("LAST_UPDT_OPRID").HasMaxLength(30);
            Property(g => g.CreatedDatetime).HasColumnName("CREATED_DTTM");
            Property(g => g.ModifiedDatetime).HasColumnName("LAST_UPDT_DTTM");

            HasKey(k => new { k.LearningObjectiveSetId });
        }
    }
}
