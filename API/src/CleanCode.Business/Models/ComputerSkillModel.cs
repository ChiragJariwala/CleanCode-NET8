using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class ComputerSkillModel : BaseModel
    {
        public int ComputerSkillId { get; set; }
        public int? VolunteerId { get; set; }
        public int? SkillId { get; set; }
    }
}
