using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class DepartmentModel : BaseModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
