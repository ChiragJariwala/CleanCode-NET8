using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VehicleModel : BaseModel
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public string ModelName { get; set; }
        public string Owner { get; set; }
        public string OwnerMobile { get; set; }
        public int? VehicleCategoryId { get; set; }
        public int? FuelTypeId { get; set; }
        public int? SeatingCapacity { get; set; }
        public string Description { get; set; }
        public string Insurance { get; set; }
        public bool? IsExistPuc { get; set; }
        public bool? IsExistRcbook { get; set; }
    }
}
