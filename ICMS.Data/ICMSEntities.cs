using ICMS.Data.Configuration;
using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICMS.Common;
using ICMS.Entity.BaseEntities;
using System.Data.Entity.Infrastructure;

namespace ICMS.Data
{
    public class ICMSEntities : DbContext
    {
        public ICMSEntities() : base("ICMSEntities") { }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<LearningObjective> LearningObjectives { get; set; }
        public DbSet<LearningObjectiveSet> LearningObjectiveSets { get; set; }
        public DbSet<LearningObjectiveStatus> LearningObjectiveStatuses { get; set; }
        public DbSet<LearningObjectiveQuestion> LearningObjectiveQuestions { get; set; }
        public DbSet<LearningObjectiveAnswer> LearningObjectiveAnswers { get; set; }
        public DbSet<ActivityQuestion> ActivityQuestions { get; set; }
        public DbSet<ActivityAnswer> ActivityAnswers { get; set; }
        public DbSet<StudentReflectionHeader> StudentReflectionHeaders { get; set; }
        public DbSet<KeyValueDescription> KeyValueDescriptions { get; set; }
        public DbSet<OverallSummary> OverallSummaries { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ReflectionSet> ReflectionSets { get; set; }
        public DbSet<StudentProgramme> StudentProgrammes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public virtual void Commit()
        {
            AddTimestamps();
            base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                            .Where(x => (x.Entity.HasProperty("CreatedDatetime") || x.Entity.HasProperty("CreatedBy")
                            || x.Entity.HasProperty("ModifiedDatetime") || x.Entity.HasProperty("ModifiedBy")) && 
                                (x.State == EntityState.Added || x.State == EntityState.Modified));

            var query = ((IObjectContextAdapter)this).ObjectContext.CreateQuery<DateTime>("CurrentDateTime() ");
            DateTime dbDate = query.AsEnumerable().First();

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((CreatedModifiedBase)entity.Entity).CreatedDatetime = dbDate;
                    ((CreatedModifiedBase)entity.Entity).CreatedBy = GlobalVariables.StudentUsername;
                    ((CreatedModifiedBase)entity.Entity).ModifiedDatetime = null;
                    ((CreatedModifiedBase)entity.Entity).ModifiedBy = "";
                }
                else
                {
                    ((CreatedModifiedBase)entity.Entity).ModifiedDatetime = dbDate;
                    ((CreatedModifiedBase)entity.Entity).ModifiedBy = GlobalVariables.StudentUsername;
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActivityAnswerConfiguration());
            modelBuilder.Configurations.Add(new ActivityConfiguration());
            modelBuilder.Configurations.Add(new ActivityQuestionConfiguration());
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new KeyValueDescriptionConfiguration());
            modelBuilder.Configurations.Add(new LearningIndicatorConfiguration());
            modelBuilder.Configurations.Add(new LearningObjectiveAnswerConfiguration());
            modelBuilder.Configurations.Add(new LearningObjectiveConfiguration());
            modelBuilder.Configurations.Add(new LearningObjectiveQuestionConfiguration());
            modelBuilder.Configurations.Add(new LearningObjectiveSetConfiguration());
            modelBuilder.Configurations.Add(new LearningObjectiveStatusConfiguration());
            modelBuilder.Configurations.Add(new OrganizationConfiguration());
            modelBuilder.Configurations.Add(new OverallSummaryConfiguration());
            modelBuilder.Configurations.Add(new ReflectionSetConfiguration());
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new AdminConfiguration());
            modelBuilder.Configurations.Add(new StudentProgrammeConfiguration());
            modelBuilder.Configurations.Add(new StudentReflectionHeaderConfiguration());
        }
    }
}
