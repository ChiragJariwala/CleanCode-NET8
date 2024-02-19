using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class SubDepartmentModel : BaseModel
    {
        public int SubDepartmentId { get; set; }
        public string SubDepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public bool? IsActive { get; set; }
    }
}
