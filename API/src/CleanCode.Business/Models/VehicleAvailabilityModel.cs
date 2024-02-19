using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VehicleAvailabilityModel : BaseModel
    {
        public int VehicleAvailabilityId { get; set; }
        public int? VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
