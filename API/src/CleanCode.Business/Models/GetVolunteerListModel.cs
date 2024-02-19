using CleanCode.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Business.Models
{
    public class GetVolunteerListModel : BaseModel
    {
        //public int VolunteerId { get; set; }
        //public Byte[] ProfileImage { get; set; }
        //public string Name { get; set; }
        //public string CityName { get; set; }
        //public string DepartmentName { get; set; }
        //public bool IsActive { get; set; }
        //public string VolunteerCode { get; set; }
        //public string Mobile1Number { get; set; }
        //public string Mobile2Number { get; set; }

        public int VolunteerId { get; set; }
        public string VolunteerCode { get; set; }        
        public string FirstName { get; set; }
        public string MiddleName { get; set; }        
        public string LastName { get; set; }      
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsActive { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int? TalukaId { get; set; }
        public int? DistrictId { get; set; }
        public int? StateId { get; set; }        
        public int? ZipcodeId { get; set; }
        public int? CountryId { get; set; }        
        public string Mobile1Number { get; set; }
        public string Mobile2Number { get; set; }
        public string WhatsappNo { get; set; }
        public bool? IsKaryakar { get; set; }
        public string KaryakarHoddo { get; set; }
        public string Xetra { get; set; }
        public string Zone { get; set; }        
        public int? EducationId { get; set; }
        public bool IsCurrent { get; set; }
        public string EducationDetail { get; set; }
        public bool IsHindiSpeaking { get; set; }
        public bool IsHindiWritting { get; set; }
        public bool IsEnglishSpeaking { get; set; }
        public bool IsEnglishWritting { get; set; }
        public int? ProfessionId { get; set; }
        public string ProfessionDetail { get; set; }
        public string VehicleInfo { get; set; }
        public int? ExpectedSevaDays { get; set; }
        public int? SevaTime { get; set; }
        public int VolunteerProfileImageId { get; set; }
        //public byte[] ProfileImage { get; set; }

        //public string ProfileImageFile { get; set; }

        public string ProfileImage { get; set; }

        public int? DepartmentCount { get; set; }
        public int CityId { get; set; }
        public string SkillId { get; set; }

        public string ComputerSkillId { get; set; }

        public int VolunteerOtherDetailId { get; set; }        
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

        public int VolunteerSatsangId { get; set; }
        public string KarykartaNumber { get; set; }
        public string Region { get; set; }
        public string KaryakarZone { get; set; }
        public string KaryakarXetra { get; set; }
        public string KarykarDesignation { get; set; }
        public string Location { get; set; }
        public string LocationCode { get; set; }
        public bool? IsAttendWeeklySabha { get; set; }
        public bool? IsDoPujaDaily { get; set; }
    }
}
