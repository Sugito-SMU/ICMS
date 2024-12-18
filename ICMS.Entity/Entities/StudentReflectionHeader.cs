using ICMS.Entity.BaseEntities;

namespace ICMS.Entity.Entities
{
    public class StudentReflectionHeader : CreatedModifiedBase
    {
        public string StudentId { get; set; }
        public string ActivityId { get; set; }
        public string ActivityStatusCode{ get; set; }
    }
}
