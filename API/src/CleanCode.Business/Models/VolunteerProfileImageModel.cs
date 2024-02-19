using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VolunteerProfileImageModel : BaseModel
    {
        public int VolunteerProfileImageId { get; set; }
        public int VolunteerId { get; set; }
        public byte[] ProfileImage { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public string FileType { get; set; }
    }
}
