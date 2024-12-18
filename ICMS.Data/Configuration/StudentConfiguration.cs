using ICMS.Entity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ICMS.Data.Configuration
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            ToTable("PS_SIS_STDBIO_VW");
            Property(g => g.StudentId).HasColumnName("EMPLID").IsRequired().HasMaxLength(11);
            Property(g => g.FullName).HasColumnName("SIS_NAME").IsRequired().HasMaxLength(66);
            Property(g => g.NtLoginId).HasColumnName("OPRID").IsRequired().HasMaxLength(30);
            Property(g => g.SmuEmail).HasColumnName("EMAIL_ADDR").HasMaxLength(70);
            Ignore(g => g.AdmitTerm);

            HasKey(k => k.StudentId);
        }
    }
}