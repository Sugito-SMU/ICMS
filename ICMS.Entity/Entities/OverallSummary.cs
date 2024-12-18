
namespace ICMS.Entity.Entities
{
    public class OverallSummary
    {
        public string StudentId { get; set; }
        public string CareerCode { get; set; }
        public string ProgrammeCode { get; set; }
        public string Description { get; set; }
        public string ActivityTypeCode { get; set; }
        public decimal UnitRequired { get; set; }
        public decimal? UnitRecognised { get; set; }
        public decimal ProBono { get; set; }
        public string UnitType { get; set; }
        public decimal UnitRemaining { get; set; }
        public string OverallStatusCode { get; set; }
        public string OverallStatusDescription { get; set; }
    }
}
