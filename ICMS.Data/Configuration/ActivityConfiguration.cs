using ICMS.Entity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ICMS.Data.Configuration
{
    class ActivityConfiguration : EntityTypeConfiguration<Activity>
    {
        public ActivityConfiguration()
        {
            ToTable("PS_SIS_IC_PARTIC_V");
            Property(g => g.StudentId).HasColumnName("EMPLID").IsRequired().HasMaxLength(11);
            Property(g => g.ActivityId).HasColumnName("SIS_INTPART_ID").IsRequired().HasMaxLength(32);
            Property(g => g.ActivityTypeCode).HasColumnName("SIS_INTERN_TYPE").IsRequired().HasMaxLength(2);
            Property(g => g.Description).HasColumnName("SIS_POS_DESCR");
            Property(g => g.PositionTitle).HasColumnName("SIS_POS_TITLE").IsRequired().HasMaxLength(100);
            Property(g => g.StartDate).HasColumnName("START_DATE");
            Property(g => g.EndDate).HasColumnName("END_DATE");
            Property(g => g.OrganisationId).HasColumnName("SIS_ORG_ID").IsRequired().HasMaxLength(32);
            Property(g => g.CountryCode).HasColumnName("COUNTRY").IsRequired().HasMaxLength(3);
            Property(g => g.ProgrammeCode).HasColumnName("ACAD_PROG").HasMaxLength(5);
            Property(g => g.Duration).HasColumnName("SIS_UNITS_COMPLETE").IsRequired().HasPrecision(6, 1);
            Property(g => g.UnitRecognised).HasColumnName("SIS_UNITS_COMP").HasPrecision(6, 1);
            Property(g => g.ProBonoRecognised).HasColumnName("SIS_UNIT_COMP1").IsRequired().HasPrecision(6, 1);
            Property(g => g.OverallGradeCode).HasColumnName("SIS_EVAL_ASSM").HasMaxLength(254);
            Ignore(g => g.StatusCode);
            Ignore(g => g.StatusDescription);
            Ignore(g => g.ActivityTypeDescription);
            Ignore(g => g.CountryName);
            Ignore(g => g.OrganisationName);
            Ignore(g => g.OverallGradeDescription);
            Ignore(g => g.LearningObjectives);
            Ignore(g => g.ActivityQuestions);
            Ignore(g => g.Unit);
            Ignore(g => g.OverallSummaryStatus);
            Ignore(g => g.Url);

            HasKey(k => new { k.ActivityId, k.StudentId });
        }
    }
}
