using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class BillEntryModel : BaseModel
    {
        public int BillEntryId { get; set; }
        public string BillNumber { get; set; }
        public DateTime BillDate { get; set; }
        public int BillBatchId { get; set; }
        public decimal Amount { get; set; }
        public byte[] BillPhoto { get; set; }
        public string Description { get; set; }
        public int? BillSpentBy { get; set; }
        public int SignedBySant { get; set; }
        public string Remarks { get; set; }
        public int SrNo { get; set; }
    }
}
