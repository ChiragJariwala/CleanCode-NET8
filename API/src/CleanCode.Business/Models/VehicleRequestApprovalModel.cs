using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VehicleRequestApprovalModel : BaseModel
    {
        public int VehicleRequestApprovalId { get; set; }
        public int? VehicleRequestId { get; set; }
        public int? RequestId { get; set; }
        public bool? IsApproved { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string Remark { get; set; }
        public int? VehicleId { get; set; }
    }
}
