using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Business.Models
{
    public class ShiftsByVolunteerModel
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

        public bool IsActive { get; set; }
        public int VolunteerId { get; set; }
        public string VolunteerCode { get; set; }
    }
}
