using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class FuelTypeModel : BaseModel
    {
        public int FuelTypeId { get; set; }
        public string FuelTypeName { get; set; }
    }
}
