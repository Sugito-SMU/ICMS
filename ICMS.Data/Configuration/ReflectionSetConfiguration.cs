using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    class ReflectionSetConfiguration : EntityTypeConfiguration<ReflectionSet>
    {
        public ReflectionSetConfiguration()
        {
            ToTable("PS_SIS_IC_RS_VW");
            Property(g => g.ReflectionSetId).HasColumnName("SIS_RS_ID").IsRequired();
            Property(g => g.ActivityTypeCode).HasColumnName("SIS_INTERN_TYPE").IsRequired().HasMaxLength(2);
            Property(g => g.Description).HasColumnName("DESCR50").IsRequired().HasMaxLength(50);
            Property(g => g.PostReflectionHeader).HasColumnName("SIS_POST_HEADER").IsRequired();
            Property(g => g.CreatedBy).HasColumnName("CREATEOPRID").HasMaxLength(30);
            Property(g => g.ModifiedBy).HasColumnName("LAST_UPDT_OPRID").HasMaxLength(30);
            Property(g => g.CreatedDatetime).HasColumnName("CREATED_DTTM");
            Property(g => g.ModifiedDatetime).HasColumnName("LAST_UPDT_DTTM");

            HasKey(k => new { k.ReflectionSetId });
        }
    }
}
