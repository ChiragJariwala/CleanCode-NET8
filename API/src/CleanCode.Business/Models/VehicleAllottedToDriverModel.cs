using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VehicleAllottedToDriverModel : BaseModel
    {
        public int VehicleAllottedToDriverId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public string LicenceNumber { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public DateTime ServiceEndDate { get; set; }
        public string Remarks { get; set; }
        public bool? IsActive { get; set; }
    }
}
