using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VolunteerSkillModel : BaseModel
    {
        public int VolunteerSkillId { get; set; }
        public int VolunteerId { get; set; }
        public int SkillId { get; set; }
    }
}
