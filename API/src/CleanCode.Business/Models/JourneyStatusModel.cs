using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class JourneyStatusModel : BaseModel
    {
        public int JourneyStatusId { get; set; }
        public string JourneyStatusName { get; set; }
    }
}
