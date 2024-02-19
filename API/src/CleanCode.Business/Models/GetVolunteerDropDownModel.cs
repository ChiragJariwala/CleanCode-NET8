using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class GetVolunteerDropDownModel : BaseModel
    {
        public int VolunteerId { get; set; }

        public string Name { get; set; }

        public string VolunteerCode { get; set; }

        public string ProfileImage { get; set; }

        public string MobileNo { get; set; }
    }
}
