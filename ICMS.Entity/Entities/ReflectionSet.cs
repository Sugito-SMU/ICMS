using ICMS.Entity.BaseEntities;

namespace ICMS.Entity.Entities
{
    public class ReflectionSet : CreatedModifiedBase
    {
        public int ReflectionSetId { get; set; }
        public string ActivityTypeCode { get; set; }
        public string Description { get; set; }
        public string PostReflectionHeader { get; set; }
    }
}
