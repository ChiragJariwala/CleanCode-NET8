using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class RequestModuleMappingModel : BaseModel
    {
        public int ReqMapId { get; set; }
        public int DepartmentId { get; set; }
        public int ReuestTypeId { get; set; }
        public int? SubReuestTypeId { get; set; }
        public string RedirectionPageReferance { get; set; }
        public string RequestDescriptions { get; set; }
        public bool? IsActive { get; set; }
    }
}
