using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class KilometerLogModel : BaseModel
    {
        public int KilometerLogId { get; set; }
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public int KilometerNumber { get; set; }
        public int? DriverId { get; set; }
        public string KilometerPhoto { get; set; }
    }
}
