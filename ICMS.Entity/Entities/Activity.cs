using System;
using System.Collections.Generic;

namespace ICMS.Entity.Entities
{
    public class Activity
    {
        public string ActivityId { get; set; }
        public string StudentId { get; set; }
        public string ActivityTypeCode { get; set; }
        public string ActivityTypeDescription { get; set; }
        public string Description { get; set; }
        public string PositionTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public decimal Duration { get; set; }
        public decimal? UnitRecognised { get; set; }
        public decimal ProBonoRecognised { get; set; }
        public string OverallGradeCode { get; set; }
        public string OverallGradeDescription { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Unit { get; set; }
        public string OverallSummaryStatus { get; set; }
        public string ProgrammeCode { get; set; }
        public string Url { get; set; }

        public List<KeyValuePair<string, decimal?>> UnitsRecognised { get; set; }
        public List<LearningObjective> LearningObjectives { get; set; }
        public List<ActivityQuestion> ActivityQuestions { get; set; }
    }
}
