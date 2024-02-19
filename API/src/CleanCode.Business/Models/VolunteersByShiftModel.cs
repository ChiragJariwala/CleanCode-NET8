﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Business.Models
{
    public class ShiftvolunteersModel
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

        public bool? ShiftVolunteerIsActive { get; set; }

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
        public int? CityId { get; set; }
    }
}
