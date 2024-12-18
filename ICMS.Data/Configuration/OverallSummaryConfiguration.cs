using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Data.Configuration
{
    public class OverallSummaryConfiguration : EntityTypeConfiguration<OverallSummary>
    {
        public OverallSummaryConfiguration()
        {
            ToTable("PS_SIS_IC_PRGRD_VW");
            Property(g => g.StudentId).HasColumnName("EMPLID").HasMaxLength(11);
            Property(g => g.CareerCode).HasColumnName("ACAD_CAREER").HasMaxLength(4);
            Property(g => g.ProgrammeCode).HasColumnName("ACAD_PROG").HasMaxLength(5);
            Property(g => g.ActivityTypeCode).HasColumnName("SIS_INTERN_TYPE").HasMaxLength(2);
            Property(g => g.UnitRequired).HasColumnName("SIS_MINIMUM_UNITS").HasPrecision(4, 1);
            Property(g => g.UnitRecognised).HasColumnName("SIS_UNITS_COMP").HasPrecision(38, 1);
            Property(g => g.UnitType).HasColumnName("SIS_UNIT_TYPE").IsRequired().HasMaxLength(2);
            Property(g => g.OverallStatusCode).HasColumnName("SIS_GRADE_STATUS").HasMaxLength(2);
            Ignore(g => g.Description);
            Ignore(g => g.ProBono);
            Ignore(g => g.UnitRemaining);
            Ignore(g => g.OverallStatusDescription);

            HasKey(k => new { k.StudentId, k.ProgrammeCode });
        }
    }
}