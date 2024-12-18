using ICMS.Entity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ICMS.Data.Configuration
{
    public class AdminConfiguration : EntityTypeConfiguration<Admin>
    {
        public AdminConfiguration()
        {
            ToTable("PS_SIS_IC_USER_VW");
            Property(g => g.Username).HasColumnName("OPRID").IsRequired().HasMaxLength(30);

            HasKey(k => k.Username);
        }
    }
}