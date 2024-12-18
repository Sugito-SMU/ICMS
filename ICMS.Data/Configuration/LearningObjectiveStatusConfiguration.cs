using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    public class LearningObjectiveStatusConfiguration : EntityTypeConfiguration<LearningObjectiveStatus>
    {
        public LearningObjectiveStatusConfiguration()
        {
            ToTable("PS_SIS_IC_SRLO_VW");
            Property(g => g.LearningObjectiveId).HasColumnName("SIS_LO_ID").IsRequired();
            Property(g => g.ActivityId).HasColumnName("SIS_INTPART_ID").IsRequired().HasMaxLength(32);
            Property(g => g.StudentId).HasColumnName("EMPLID").IsRequired().HasMaxLength(11);
            Property(g => g.IS_LO_ON_TGT).HasColumnName("SIS_IS_LO_ON_TGT");
            Property(g => g.IS_LO_ACHV).HasColumnName("SIS_IS_LO_ACHV");
            Property(g => g.CreatedBy).HasColumnName("CREATEOPRID").HasMaxLength(30);
            Property(g => g.ModifiedBy).HasColumnName("LAST_UPDT_OPRID").HasMaxLength(30);
            Property(g => g.CreatedDatetime).HasColumnName("CREATED_DTTM");
            Property(g => g.ModifiedDatetime).HasColumnName("LAST_UPDT_DTTM");
            Ignore(g => g.IsMidActivityOnTarget);
            Ignore(g => g.IsObjectiveAchieved);

            HasKey(k => new { k.LearningObjectiveId, k.ActivityId, k.StudentId });
        }
    }
}