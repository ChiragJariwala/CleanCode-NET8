using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VehicleRequestModel : BaseModel
    {
        public int VehicleRequestId { get; set; }
        public int? RequestId { get; set; }
        public DateTime? JourneyStartDate { get; set; }
        public TimeSpan? JourneyStartTime { get; set; }
        public int? PickupLocationId { get; set; }
        public int? PickupSubLocationId { get; set; }
        public int? DropLocationId { get; set; }
        public int? DropSubLocationId { get; set; }
        public string StatyHours { get; set; }
        public bool IsReturnTrip { get; set; }
        public string Purpose { get; set; }
        public int? NoOfSeating { get; set; }
        public bool? IsRepeat { get; set; }
    }
}
