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
    public class OverallSummaryRepository : RepositoryBase<OverallSummary>, IOverallSummaryRepository
    {
        public OverallSummaryRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        public IEnumerable<OverallSummary> GetOverallSummary(string studentId, string activityType)
        {
            return (from sp in this.DbContext.StudentProgrammes
                    join os in this.DbContext.OverallSummaries on new { sp.CareerCode, sp.ProgrammeCode, sp.StudentId } equals new { os.CareerCode, os.ProgrammeCode, os.StudentId }
                    join d in this.DbContext.KeyValueDescriptions on new { StatusCode = os.OverallStatusCode, Key = XLATKeys.OverallStatus } equals new { StatusCode = d.FieldValue, Key = d.FieldName }
                    where sp.StudentId == studentId && os.ActivityTypeCode == activityType && sp.StatusCode == "AC"
                    select new
                    {
                        sp.ProgrammeCode,
                        sp.ProgramOrder,
                        sp.Description,
                        d.LongName,
                        os.CareerCode,
                        os.ActivityTypeCode,
                        os.StudentId,
                        os.UnitType,
                        os.OverallStatusCode,
                        os.UnitRequired,
                        os.UnitRecognised,
                        ProBono = ((sp.ProgrammeCode == ProgramCodes.ProbonoProgramCode) && (os.ActivityTypeCode == ActivityTypeCodes.CommunityService)) ?
                                        (this.DbContext.Activities
                                                        .Where(a => a.StudentId == sp.StudentId &&
                                                                    a.ActivityTypeCode == os.ActivityTypeCode &&
                                                                    ((a.ProgrammeCode == os.ProgrammeCode) || string.IsNullOrEmpty(a.ProgrammeCode)))
                                                        .Sum(a => a.ProBonoRecognised))
                                        : 0.0m
                    }
                    ).ToList()
                    .ConvertAll(r => new OverallSummary
                    {
                        OverallStatusCode = r.OverallStatusCode,
                        OverallStatusDescription = r.LongName,
                        Description = r.Description,
                        ProBono = r.ProgrammeCode == ProgramCodes.ProbonoProgramCode ? r.ProBono : 0.0m,
                        CareerCode = r.CareerCode,
                        ProgrammeCode = r.ProgrammeCode,
                        ActivityTypeCode = r.ActivityTypeCode,
                        StudentId = r.StudentId,
                        UnitType = r.UnitType,
                        UnitRecognised = r.UnitRecognised,
                        UnitRemaining = Math.Max(0, r.UnitRequired - (r.UnitRecognised.HasValue ? r.UnitRecognised.Value : 0)),
                        UnitRequired = r.UnitRequired
                    });
        }
    }

    public interface IOverallSummaryRepository : IRepository<OverallSummary>
    {
        IEnumerable<OverallSummary> GetOverallSummary(string activityId, string typeCode);
    }
}
