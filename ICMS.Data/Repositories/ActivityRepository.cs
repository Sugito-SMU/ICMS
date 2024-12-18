using ICMS.Common;
using ICMS.Data.Infrastructure;
using ICMS.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICMS.Common.ICMSConstants;

namespace ICMS.Data.Repositories
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        public ActivityRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Activity GetActivity(string activityId, string studentId)
        {
            string url = GetUrl(studentId);

            var activity = (from a in this.DbContext.Activities
                            join os in this.DbContext.OverallSummaries on new { a.StudentId, a.ActivityTypeCode, a.ProgrammeCode } equals new { os.StudentId, os.ActivityTypeCode, os.ProgrammeCode }
                            join c in this.DbContext.Countries on a.CountryCode equals c.CountryId
                            join s in this.DbContext.StudentReflectionHeaders on new { a.ActivityId, a.StudentId } equals new { s.ActivityId, s.StudentId } into rh
                            from rh2 in rh.DefaultIfEmpty()
                            join d1 in this.DbContext.KeyValueDescriptions on new { StatusCode = (rh2 == null ? (a.ProBonoRecognised > 0 ? ActivityReflectionCodes.NotApplicable : ActivityReflectionCodes.PreActivityNotStarted) : rh2.ActivityStatusCode), Key = XLATKeys.ActivityStatus } equals new { StatusCode = d1.FieldValue, Key = d1.FieldName }
                            join d2 in this.DbContext.KeyValueDescriptions on new { StatusCode = a.OverallGradeCode, Key = XLATKeys.OverallGrade } equals new { StatusCode = d2.FieldValue, Key = d2.FieldName } into grade
                            from grade2 in grade.DefaultIfEmpty()
                            join d3 in this.DbContext.KeyValueDescriptions on new { StatusCode = a.ActivityTypeCode, Key = XLATKeys.ActivityType } equals new { StatusCode = d3.FieldValue, Key = d3.FieldName }
                            join o in this.DbContext.Organizations on a.OrganisationId equals o.OrganizationId
                            where a.ActivityId == activityId && a.StudentId == studentId && os.OverallStatusCode != OverallStatusCodes.NCM
                            select new { a, c, rh2, d1, grade2, d3, o, os }).ToList();
            if (activity.Count == 0)
            {
                activity = (from a in this.DbContext.Activities
                                join os in this.DbContext.OverallSummaries on new { a.StudentId, a.ActivityTypeCode } equals new { os.StudentId, os.ActivityTypeCode}
                                join c in this.DbContext.Countries on a.CountryCode equals c.CountryId
                                join s in this.DbContext.StudentReflectionHeaders on new { a.ActivityId, a.StudentId } equals new { s.ActivityId, s.StudentId } into rh
                                from rh2 in rh.DefaultIfEmpty()
                                join d1 in this.DbContext.KeyValueDescriptions on new { StatusCode = (rh2 == null ? (a.ProBonoRecognised > 0 ? ActivityReflectionCodes.NotApplicable : ActivityReflectionCodes.PreActivityNotStarted) : rh2.ActivityStatusCode), Key = XLATKeys.ActivityStatus } equals new { StatusCode = d1.FieldValue, Key = d1.FieldName }
                                join d2 in this.DbContext.KeyValueDescriptions on new { StatusCode = a.OverallGradeCode, Key = XLATKeys.OverallGrade } equals new { StatusCode = d2.FieldValue, Key = d2.FieldName } into grade
                                from grade2 in grade.DefaultIfEmpty()
                                join d3 in this.DbContext.KeyValueDescriptions on new { StatusCode = a.ActivityTypeCode, Key = XLATKeys.ActivityType } equals new { StatusCode = d3.FieldValue, Key = d3.FieldName }
                                join o in this.DbContext.Organizations on a.OrganisationId equals o.OrganizationId
                                where a.ActivityId == activityId && a.StudentId == studentId && os.OverallStatusCode != OverallStatusCodes.NCM
                                select new { a, c, rh2, d1, grade2, d3, o, os }).ToList();
            }

            return activity.Count == 0 ? null : activity.ConvertAll(r => new Activity
            {
                ActivityId = r.a.ActivityId,
                StudentId = r.a.StudentId,
                ActivityTypeCode = r.a.ActivityTypeCode,
                ActivityTypeDescription = r.d3.LongName,
                Description = r.a.Description,
                StartDate = r.a.StartDate,
                EndDate = r.a.EndDate,
                PositionTitle = r.a.PositionTitle,
                OrganisationId = r.a.OrganisationId,
                OrganisationName = r.o.Description,
                CountryCode = r.a.CountryCode,
                CountryName = r.c.Description,
                Duration = r.a.Duration,
                UnitRecognised = r.a.UnitRecognised,
                UnitsRecognised = GetActivityProgrammes(activityId, studentId, r.a.ActivityTypeCode),
                ProBonoRecognised = r.a.ProBonoRecognised,
                StatusCode = r.rh2 == null ? ActivityReflectionCodes.PreActivityNotStarted : r.rh2.ActivityStatusCode,
                StatusDescription = r.d1?.LongName,
                OverallGradeCode = r.a.OverallGradeCode,
                OverallGradeDescription = r.a.OverallGradeCode,
                Unit = r.os.UnitType,
                OverallSummaryStatus = r.os.OverallStatusCode,
                Url = url
            }).First();
        }

        public string GetPostReflectionHeader(string studentId, string activityId)
        {
            var header = (from rs in this.DbContext.ReflectionSets
                          join a in this.DbContext.Activities on rs.ActivityTypeCode equals a.ActivityTypeCode
                          where a.ActivityId == activityId && a.StudentId == studentId
                          select rs.PostReflectionHeader);

            return header.FirstOrDefault();
        }

        public IEnumerable<Activity> GetActivities(string studentId, string activityType)
        {
            string url = GetUrl(studentId);

            var activities = (from a in this.DbContext.Activities
                              join s in this.DbContext.StudentReflectionHeaders on new { a.ActivityId, a.StudentId } equals new { s.ActivityId, s.StudentId } into rh
                              from s in rh.DefaultIfEmpty()
                              join d in this.DbContext.KeyValueDescriptions on new { StatusCode = (s == null ? (a.ProBonoRecognised > 0 ? ActivityReflectionCodes.NotApplicable : ActivityReflectionCodes.PreActivityNotStarted) : s.ActivityStatusCode), Key = XLATKeys.ActivityStatus } equals new { StatusCode = d.FieldValue, Key = d.FieldName }
                              group a by new { a.ActivityId, a.StudentId, a.PositionTitle, a.StartDate, a.ProBonoRecognised, a.EndDate, d.FieldValue, a.ActivityTypeCode, s.ActivityStatusCode, d.LongName } into acts
                              where acts.Key.StudentId == studentId && acts.Key.ActivityTypeCode == activityType && acts.Key.FieldValue != OverallStatusCodes.NCM
                              select acts).ToList()
                        .ConvertAll(r => new Activity
                        {
                            ActivityId = r.Key.ActivityId,
                            StudentId = r.Key.StudentId,
                            ActivityTypeCode = r.Key.ActivityStatusCode,
                            StartDate = r.Key.StartDate,
                            EndDate = r.Key.EndDate,
                            PositionTitle = r.Key.PositionTitle,
                            OrganisationId = null,
                            CountryCode = null,
                            Duration = 0.0m,
                            UnitRecognised = null,
                            ProBonoRecognised = r.Key.ProBonoRecognised,
                            StatusCode = r.Key == null ? ActivityReflectionCodes.PreActivityNotStarted : r.Key.ActivityStatusCode,
                            StatusDescription = string.IsNullOrEmpty(url) ? r.Key.LongName : "-",
                            Url = url
                        });

            return activities;
        }

        private string GetUrl(string studentId)
        {
            var stdProg = (from sp in this.DbContext.StudentProgrammes
                           where sp.StudentId == studentId && sp.CareerCode == CareerCodes.UnderGraduate
                                && sp.ProgramOrder == 0 && Programs.Keys.Contains(sp.StatusCode)
                           select sp).FirstOrDefault();

            string url = null;
            if ((stdProg != null) && (!string.IsNullOrEmpty(stdProg.AdmitTerm)) && (Convert.ToInt32(stdProg.AdmitTerm) >= eLearnAdmitTerm))
            {
                url = System.Configuration.ConfigurationManager.AppSettings["eLearnUrl"];
            }
            return url;
        }

        private List<KeyValuePair<string, decimal?>> GetActivityProgrammes(string activityId, string studentId, string activityTypeCode)
        {
            var result = (from a in this.DbContext.Activities
                            join sp in this.DbContext.StudentProgrammes 
                                on new { a.StudentId, a.ProgrammeCode } equals new { sp.StudentId, sp.ProgrammeCode }
                    where a.ActivityId == activityId && a.StudentId == studentId && a.ActivityTypeCode == activityTypeCode
                orderby sp.ProgramOrder
                select new { sp, a.UnitRecognised }).ToList();

            return result.Select(p => new KeyValuePair<string, decimal?>(p.sp.Description, p.UnitRecognised)).ToList();
        }

        public IEnumerable<KeyValueDescription> GetActivityStatusForChangeByAdmin(List<string> statuses)
        {
            return (from s in statuses
                    join d in this.DbContext.KeyValueDescriptions on new { Key = XLATKeys.ActivityStatus, Value = s } equals new { Key = d.FieldName, Value = d.FieldValue }
                    select d).ToList();
        }
    }

    public interface IActivityRepository : IRepository<Activity>
    {
        Activity GetActivity(string activityId, string studentId);
        string GetPostReflectionHeader(string activityId, string studentId);
        IEnumerable<Activity> GetActivities(string activityId, string studentId);

        IEnumerable<KeyValueDescription> GetActivityStatusForChangeByAdmin(List<string> statuses);
    }
}
