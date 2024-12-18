using ICMS.Entity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ICMS.Data.Configuration
{
    class LearningObjectiveConfiguration : EntityTypeConfiguration<LearningObjective>
    {
        public LearningObjectiveConfiguration()
        {
            ToTable("PS_SIS_IC_LO_VW");
            Property(g => g.LearningObjectiveId).HasColumnName("SIS_LO_ID").IsRequired();
            Property(g => g.LearningObjectiveSetId).HasColumnName("SIS_LOS_ID").IsRequired();
            Property(g => g.Description).HasColumnName("SIS_LO_TEXT");
            Property(g => g.MidActivityNotOnTargetInstruction).HasColumnName("SIS_LO_INSTR");
            Property(g => g.EFF_STATUS).HasColumnName("EFF_STATUS").HasMaxLength(1);
            Ignore(g => g.IsActive);
            Ignore(g => g.IsSelected);
            Ignore(g => g.IsMidActivityOnTarget);
            Ignore(g => g.IsObjectiveAchieved);
            Ignore(g => g.LearningObjectiveStatus);

            HasKey(k => new { k.LearningObjectiveId });
        }
    }
}
