using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class RoleModel : BaseModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
