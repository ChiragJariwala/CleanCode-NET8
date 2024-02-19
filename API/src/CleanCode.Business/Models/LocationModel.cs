using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class LocationModel : BaseModel
    {
        public int LocationId { get; set; }
        public string PlaceName { get; set; }
        public string Description { get; set; }
        public int? ResponsiblePerson { get; set; }
    }
}
