using CleanCode.Business.Models.Base;

namespace CleanCode.Business.Models
{
    public class VolunteerOtherDetailModel : BaseModel
    {
        public int VolunteerOtherDetailId { get; set; }
        public int VolunteerId { get; set; }
        public string AnyLongTermIllness { get; set; }
        public bool CanComein2PhasesinSeva { get; set; }
        public bool AlternateDayLaundryIroningClothes { get; set; }
        public bool AlternateDayIroningClothes { get; set; }
        public bool StudCanComeinSummerVacation { get; set; }
        public int? StudComeinSummerSevaDay { get; set; }
        public bool StudCanComeinDiwaliVacation { get; set; }
        public int? StudCanComeinDiwaliDays { get; set; }
        public bool DoYouWorkInPolice { get; set; }
        public string PolicePost { get; set; }
        public string SevaCertificateLanguage { get; set; }
    }
}
