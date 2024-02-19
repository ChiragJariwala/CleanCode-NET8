using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class RequestStatusModel : BaseModel
    {
        public int RequestStatusId { get; set; }
        public string RequestStatusName { get; set; }
    }
}
