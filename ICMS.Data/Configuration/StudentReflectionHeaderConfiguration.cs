using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    class StudentReflectionHeaderConfiguration : EntityTypeConfiguration<StudentReflectionHeader>
    {
        public StudentReflectionHeaderConfiguration()
        {
            ToTable("PS_SIS_IC_STDRH_VW");
            Property(g => g.StudentId).HasColumnName("EMPLID").IsRequired().HasMaxLength(11);
            Property(g => g.ActivityId).HasColumnName("SIS_INTPART_ID").IsRequired().HasMaxLength(32);
            Property(g => g.ActivityStatusCode).HasColumnName("SIS_R_STATUS").HasMaxLength(2);
            Property(g => g.CreatedBy).HasColumnName("CREATEOPRID").HasMaxLength(30);
            Property(g => g.ModifiedBy).HasColumnName("LAST_UPDT_OPRID").HasMaxLength(30);
            Property(g => g.CreatedDatetime).HasColumnName("CREATED_DTTM");
            Property(g => g.ModifiedDatetime).HasColumnName("LAST_UPDT_DTTM");

            HasKey(k => new { k.StudentId, k.ActivityId });
        }
    }
}
