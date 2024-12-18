using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Entity.ViewModels
{
    public class ActivityViewModel
    {
        public string ActivityId { get; set; }
        public string Description { get; set; }
        public string ActivityTypeCode { get; set; }
        public string ActivityTypeDescription { get; set; }
        public string PositionTitle { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string OrganisationDescription { get; set; }
        public string CountryDescription { get; set; }
        public int Duration { get; set; }
        public int UnitRecognised { get; set; }
        public string OverallGradeDescription { get; set; }
        public string ReflectionStatusCode { get; set; }
        public string ReflectionStatusDescription { get; set; }
        public string Unit { get; set; }
        public bool ShowPreActivity { get; set; }
        public bool ShowMidActivity { get; set; }
        public bool ShowPostActivity { get; set; }
        public bool EnablePreActivity { get; set; }
        public bool EnableMidActivity { get; set; }
        public bool EnablePostActivity { get; set; }
        public string Url { get; set; }

        public List<KeyValuePair<string, decimal?>> UnitsRecognised { get; set; }
    }
}
