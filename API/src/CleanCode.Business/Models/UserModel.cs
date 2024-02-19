using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class UserModel : BaseModel
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public int VolunteerId { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public int RoleId { get; set; }
    }
}
