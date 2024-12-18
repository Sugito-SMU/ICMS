using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    public class LearningIndicatorConfiguration : EntityTypeConfiguration<LearningIndicator>
    {
        public LearningIndicatorConfiguration()
        {
            ToTable("PS_SIS_IC_LI_VW");
            Property(g => g.LearningIndicatorId).HasColumnName("SIS_LI_ID").IsRequired();
            Property(g => g.LearningObjectiveId).HasColumnName("SIS_LO_ID").IsRequired();
            Property(g => g.Description).HasColumnName("SIS_LI_TEXT").HasMaxLength(50);
            Property(g => g.SequenceNo).HasColumnName("SEQNUM");
            Property(g => g.EFF_STATUS).HasColumnName("EFF_STATUS").HasMaxLength(1);
            Property(g => g.CreatedBy).HasColumnName("CREATEOPRID").HasMaxLength(30);
            Property(g => g.ModifiedBy).HasColumnName("LAST_UPDT_OPRID").HasMaxLength(30);
            Property(g => g.CreatedDatetime).HasColumnName("CREATED_DTTM");
            Property(g => g.ModifiedDatetime).HasColumnName("LAST_UPDT_DTTM");
            Ignore(g => g.IsActive);

            HasKey(k => new { k.LearningIndicatorId });
        }
    }
}