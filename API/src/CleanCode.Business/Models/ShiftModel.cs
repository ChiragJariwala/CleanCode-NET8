using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class ShiftModel : BaseModel
    {
        public int ShiftId { get; set; }
        public int DepartmentId { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public decimal? Hours { get; set; }
        public int LocationId { get; set; }
        public int SubLocationId { get; set; }
        public string ReponsibiltyRemark { get; set; }
        public int ReportTo { get; set; }
        public int? Leader { get; set; }
        public bool? EntryByLeader { get; set; }
        public int? ShiftTypeId { get; set; }
    }
}
