using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class DepartmentLocationModel : BaseModel
    {
        public int DepartmentLocationId { get; set; }
        public int? DepartmentId { get; set; }
        public int LocationId { get; set; }
        public int SubLocationId { get; set; }
        public bool? IsActive { get; set; }
    }
}
