using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class RequestModel : BaseModel
    {
        public int RequestId { get; set; }
        public DateTime RequestedDate { get; set; }
        public int FromPerson { get; set; }
        public int FromDepartment { get; set; }
        public int ToDepartment { get; set; }
        public int ToPerson { get; set; }
        public int ContactPerson { get; set; }
        public int RequestTypeId { get; set; }
        public string Brief { get; set; }
        public int? LocationId { get; set; }
        public int RequestStatusId { get; set; }
        public int? SubLocationId { get; set; }
    }
}
