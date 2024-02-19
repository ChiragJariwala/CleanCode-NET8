using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VehicleBookingModel : BaseModel
    {
        public int VehicleBookingId { get; set; }
        public int VehicleRequestApprovalId { get; set; }
        public int VehicleCategoryId { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public DateTime TripStartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public string Remark { get; set; }
    }
}
