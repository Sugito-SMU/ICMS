using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    class OrganizationConfiguration : EntityTypeConfiguration<Organization>
    {
        public OrganizationConfiguration()
        {
            ToTable("PS_SIS_ORG_VW");
            Property(g => g.OrganizationId).HasColumnName("SIS_ORG_ID").IsRequired().HasMaxLength(32);
            Property(g => g.Description).HasColumnName("SIS_ORG_DESCR").IsRequired().HasMaxLength(100);

            HasKey(k => new { k.OrganizationId });
        }
    }
}
