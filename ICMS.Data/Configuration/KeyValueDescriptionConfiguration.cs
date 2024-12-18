using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    class KeyValueDescriptionConfiguration : EntityTypeConfiguration<KeyValueDescription>
    {
        public KeyValueDescriptionConfiguration()
        {
            ToTable("PS_SIS_XLAT_VW");
            Property(g => g.FieldName).HasColumnName("FIELDNAME").IsRequired().HasMaxLength(18);
            Property(g => g.FieldValue).HasColumnName("FIELDVALUE").IsRequired().HasMaxLength(4);
            Property(g => g.EffectiveDate).HasColumnName("EFFDT");
            Property(g => g.LongName).HasColumnName("XLATLONGNAME").IsRequired().HasMaxLength(30);
            Property(g => g.ShortName).HasColumnName("XLATSHORTNAME").IsRequired().HasMaxLength(10);
            Property(g => g.LASTUPDDTTM).HasColumnName("LASTUPDDTTM");
            Property(g => g.LASTUPDOPRID).HasColumnName("LASTUPDOPRID").HasMaxLength(30);
            Property(g => g.SYNCID).HasColumnName("SYNCID").IsRequired();
            //Property(g => g.IsActive).HasColumnAnnotation("BackingField", "EFF_STATUS");

            Ignore(g => g.IsActive);

            HasKey(k => new { k.FieldName, k.FieldValue });
        }
    }
}
