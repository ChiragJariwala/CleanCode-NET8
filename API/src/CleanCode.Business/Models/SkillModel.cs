using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class SkillModel : BaseModel
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int? SkillTypeId { get; set; }
    }
}
