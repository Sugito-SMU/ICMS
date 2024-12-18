using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            ToTable("PS_COUNTRY1_VW");
            Property(g => g.CountryId).HasColumnName("COUNTRY_CD").IsRequired().HasMaxLength(3);
            Property(g => g.Description).HasColumnName("DESCR").IsRequired().HasMaxLength(30);
            Property(g => g.DescriptionShort).HasColumnName("DESCRSHORT").IsRequired().HasMaxLength(10);

            HasKey(k => new { k.CountryId });
        }
    }
}
