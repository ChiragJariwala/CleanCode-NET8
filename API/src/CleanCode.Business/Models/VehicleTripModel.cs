using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VehicleTripModel : BaseModel
    {
        public int VehicleTripId { get; set; }
        public int VehicleBookingId { get; set; }
        public int DriverId { get; set; }
        public DateTime? JourneyStartOn { get; set; }
        public int? JournetStatusId { get; set; }
        public DateTime? JourneyEndOn { get; set; }
    }
}
