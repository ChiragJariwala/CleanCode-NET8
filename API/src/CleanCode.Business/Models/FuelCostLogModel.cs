using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class FuelCostLogModel : BaseModel
    {
        public int FuelCostLogId { get; set; }
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public string FilledUnit { get; set; }
        public int Amount { get; set; }
        public int? FilledBy { get; set; }
        public string Note { get; set; }
        public string ReceiptPhoto { get; set; }
    }
}
