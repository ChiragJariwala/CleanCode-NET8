using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VolunteerSatsangModel : BaseModel
    {
        public int VolunteerSatsangId { get; set; }
        public int VolunteerId { get; set; }
        public string KarykartaNumber { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string Xetra { get; set; }
        public string KarykarDesignation { get; set; }
        public string Location { get; set; }
        public string LocationCode { get; set; }
        public bool? IsAttendWeeklySabha { get; set; }
        public bool? IsDoPujaDaily { get; set; }
        public bool? IsActive { get; set; }
    }
}
