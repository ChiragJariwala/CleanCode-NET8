using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class BillBatchModel : BaseModel
    {
        public int BillBatchId { get; set; }
        public decimal TotalAmount { get; set; }
        public int DepartmentId { get; set; }
        public int? DepartmentPerson { get; set; }
        public int TransactionTypeId { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime VerifiedAt { get; set; }
        public int PaymentStatusId { get; set; }
        public int? PaymentModeId { get; set; }
        public int AmountGivenTo { get; set; }
        public int AmountGivenBy { get; set; }
        public DateTime AmountGivenAt { get; set; }
        public string Remarks { get; set; }
    }
}
