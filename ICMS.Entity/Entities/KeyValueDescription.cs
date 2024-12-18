using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICMS.Entity.Entities
{
    public class KeyValueDescription
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public DateTime EffectiveDate { get; set; }
        protected string EFF_STATUS { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public DateTime LASTUPDDTTM { get; set; }
        public string LASTUPDOPRID { get; set; }
        public int SYNCID { get; set; }
        
        public bool IsActive
        {
            get { return EFF_STATUS == "A"; }
            set { EFF_STATUS = value ? "A" : "I"; }
        }
    }
}
