using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VolunteerTxnModel : BaseModel
    {
        public int VolunteerTxnId { get; set; }
        public int VolunteerId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public bool? IsActive { get; set; }
        public int? SubDepartmentId { get; set; }
    }
}
