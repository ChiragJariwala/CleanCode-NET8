using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VehicleCategoryModel : BaseModel
    {
        public int VehicleCategoryId { get; set; }
        public string VehicleCategoryName { get; set; }
        public string Description { get; set; }
    }
}
