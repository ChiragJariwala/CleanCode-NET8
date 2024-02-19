using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class ShiftVolunteerModel : BaseModel
    {
        public int ShiftVolunteerId { get; set; }
        public int ShiftId { get; set; }
        public int VolunteerId { get; set; }
        public int IsActive { get; set; }
    }
}
