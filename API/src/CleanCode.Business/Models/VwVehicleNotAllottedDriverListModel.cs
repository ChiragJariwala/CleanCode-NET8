using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VwVehicleNotAllottedDriverListModel : BaseModel
    {
        public int VolunteerId { get; set; }
        public string VolunteerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public string MobileNo { get; set; }
    }
}
