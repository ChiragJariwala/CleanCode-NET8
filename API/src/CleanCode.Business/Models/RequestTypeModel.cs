using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class RequestTypeModel : BaseModel
    {
        public int RequestTypeId { get; set; }
        public string RequestTypeName { get; set; }
        public int DepartmentId { get; set; }
    }
}
