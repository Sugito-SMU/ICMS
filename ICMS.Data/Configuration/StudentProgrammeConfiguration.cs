using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    class StudentProgrammeConfiguration : EntityTypeConfiguration<StudentProgramme>
    {
        public StudentProgrammeConfiguration()
        {
            ToTable("PS_SIS_STDDEGR_VW");
            Property(g => g.StudentId).HasColumnName("EMPLID").IsRequired().HasMaxLength(11);
            Property(g => g.CareerCode).HasColumnName("ACAD_CAREER").IsRequired().HasMaxLength(4);
            Property(g => g.ProgramOrder).HasColumnName("STDNT_CAR_NBR").IsRequired();
            Property(g => g.ProgrammeCode).HasColumnName("ACAD_PROG").IsRequired().HasMaxLength(5);
            Property(g => g.Description).HasColumnName("DESCRSHORT").IsRequired().HasMaxLength(10);
            Property(g => g.StatusCode).HasColumnName("PROG_STATUS").IsRequired().HasMaxLength(4);
            Property(g => g.AdmitTerm).HasColumnName("ADMIT_TERM").IsRequired().HasMaxLength(4);

            HasKey(k => new { k.StudentId, k.CareerCode, k.ProgramOrder });
        }
    }
}
