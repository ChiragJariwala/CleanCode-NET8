using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Infrastructure.Repositories
{
	internal class SQLQueries
	{
		internal const string SelectPersonSkillHobbyByPersonSkillId = @"SELECT [PersonSkillHobbyId], [PersonId], [SkillHobbyId], [SkillHobbyLevel], [SkillHobbyIdAndLevel] FROM [Person].[PersonSkillHobby] WITH(NOLOCK) where  IsDeleted = 0 and [PersonSkillHobbyId] = @PersonSkillHobbyId";
		internal const string GetVolunteerDropDownList = @"SELECT vol.VolunteerId,vol.VolunteerCode,vol.FirstName + ' ' + vol.LastName as Name,ProfileImage,vol.Mobile1Number as MobileNo FROM [dbo].[Volunteer] as Vol WITH(NOLOCK);";
		internal const string SelectShiftVolunteersByShiftId = @"SELECT sv.IsActive as ShiftVolunteerIsActive,S.*,vol.* FROM [stihydra_balnagari].[dbo].[ShiftVolunteer] SV inner join [dbo].[Shift] S on SV.ShiftId=S.ShiftId inner join [dbo].[Volunteer] vol on SV.VolunteerId = vol.VolunteerId where SV.ShiftId = @ShiftId";
		internal const string SelectShiftVolunteersByVolunteerId = @"SELECT sv.*,S.*,vol.VolunteerId,VolunteerCode FROM [stihydra_balnagari].[dbo].[ShiftVolunteer] SV inner join [dbo].[Shift] S on SV.ShiftId=S.ShiftId inner join [dbo].[Volunteer] vol on SV.VolunteerId = vol.VolunteerId where SV.VolunteerId = @VolunteerId";
		internal const string SelectUserByCredentials = "Select * from [dbo].[User] where MobileNo = @MobileNo and Password = @Password and IsActive = 1";
		internal const string SelectUserDepartmentDesignations = @"Select * from [dbo].[VolunteerTxn] vtxn
inner join [dbo].[Department] dpt on vtxn.DepartmentId = dpt.DepartmentId
inner join [dbo].[Designation] dsg on vtxn.DesignationId = dsg.DesignationId
where vtxn.VolunteerId = @VolunteerId";
		internal const string SelectVolunteerByMobileNo = @"SELECT vol.*,                                                  
													city.CityName, 													
													STUFF((SELECT ','+ cast(SkillId as nvarchar(200)) FROM [dbo].[VolunteerSkill] vskl
													WHERE vskl.VolunteerId=vol.VolunteerId FOR XML PATH('')), 1, 1, '') AS SkillId,
													STUFF((SELECT ','+ cast(SkillId as nvarchar(200)) FROM [dbo].[ComputerSkill] vskl
													WHERE vskl.VolunteerId=vol.VolunteerId FOR XML PATH('')), 1, 1, '') AS ComputerSkillId,
													(Select count(*) from [dbo].[VolunteerTxn] where VolunteerId = vol.VolunteerId) As 'DepartmentCount',
													vol.IsActive,vod.*,
													vos.VolunteerSatsangId,vos.KarykartaNumber,vos.Region,vos.KarykarDesignation,vos.Location,vos.Locationcode,vos.IsAttendWeeklySabha,
													vos.IsDoPujaDaily,vos.Zone as 'KaryakarZone',vos.Xetra as 'KaryakarXetra'
													FROM [dbo].[Volunteer] as Vol WITH(NOLOCK) 
													Left JOIN [dbo].[City] as City  WITH(NOLOCK) ON Vol.CityId = City.CityId														
													Left JOIN [dbo].[VolunteerOtherDetail] as vod  WITH(NOLOCK) ON vod.VolunteerId = vol.VolunteerId
													Left JOIN [dbo].[VolunteerSatsang] as vos  WITH(NOLOCK) ON vos.VolunteerId = vol.VolunteerId	
													where Mobile1Number = @MobileNo";
		internal const string GetVolunteerSkillsList = @"Select vol.VolunteerId,vol.VolunteerCode,vol.FirstName,vol.LastName,vskl.VolunteerSkillId,vskl.SkillId,skl.SkillName from [dbo].[Volunteer] vol inner join
[dbo].[VolunteerSkill] vskl on vol.VolunteerId = vskl.VolunteerId inner join
[dbo].[Skill] skl on vskl.SkillId = skl.SkillId
where vol.VolunteerId = @VolunteerId";
		internal const string GetVolunteerComputerSkillsList = @"Select vol.VolunteerId,vol.VolunteerCode,vol.FirstName,vol.LastName,vskl.ComputerSkillId,vskl.SkillId,skl.SkillName from [dbo].[Volunteer] vol inner join
[dbo].[ComputerSkill] vskl on vol.VolunteerId = vskl.VolunteerId inner join
[dbo].[Skill] skl on vskl.SkillId = skl.SkillId
where vol.VolunteerId = @VolunteerId";
		internal const string GetVolunteerById = @"SELECT vol.*,                                                  
													city.CityName, 													 
													STUFF((SELECT ','+ cast(SkillId as nvarchar(200)) FROM [dbo].[VolunteerSkill] vskl
													WHERE vskl.VolunteerId=vol.VolunteerId FOR XML PATH('')), 1, 1, '') AS SkillId,
													STUFF((SELECT ','+ cast(SkillId as nvarchar(200)) FROM [dbo].[ComputerSkill] vskl
													WHERE vskl.VolunteerId=vol.VolunteerId FOR XML PATH('')), 1, 1, '') AS ComputerSkillId,
													(Select count(*) from [dbo].[VolunteerTxn] where VolunteerId = vol.VolunteerId) As 'DepartmentCount',
													vol.IsActive,vod.*,
													vos.VolunteerSatsangId,vos.KarykartaNumber,vos.Region,vos.KarykarDesignation,vos.Location,vos.Locationcode,vos.IsAttendWeeklySabha,
													vos.IsDoPujaDaily,vos.Zone as 'KaryakarZone',vos.Xetra as 'KaryakarXetra'
													FROM [dbo].[Volunteer] as Vol WITH(NOLOCK) 
													Left JOIN [dbo].[City] as City  WITH(NOLOCK) ON Vol.CityId = City.CityId													
													Left JOIN [dbo].[VolunteerOtherDetail] as vod  WITH(NOLOCK) ON vod.VolunteerId = vol.VolunteerId
													Left JOIN [dbo].[VolunteerSatsang] as vos  WITH(NOLOCK) ON vos.VolunteerId = vol.VolunteerId	
													Where 
													Vol.VolunteerId=@VolunteerId";
		internal const string GetVolunteerList = @"SELECT vol.*,                                                  
													city.CityName, 													
													STUFF((SELECT ','+ cast(SkillId as nvarchar(200)) FROM [dbo].[VolunteerSkill] vskl
													WHERE vskl.VolunteerId=vol.VolunteerId FOR XML PATH('')), 1, 1, '') AS SkillId,
													STUFF((SELECT ','+ cast(SkillId as nvarchar(200)) FROM [dbo].[ComputerSkill] vskl
													WHERE vskl.VolunteerId=vol.VolunteerId FOR XML PATH('')), 1, 1, '') AS ComputerSkillId,
													(Select count(*) from [dbo].[VolunteerTxn] where VolunteerId = vol.VolunteerId) As 'DepartmentCount',
													vol.IsActive,vod.*,
													vos.VolunteerSatsangId,vos.KarykartaNumber,vos.Region,vos.KarykarDesignation,vos.Location,vos.Locationcode,vos.IsAttendWeeklySabha,
													vos.IsDoPujaDaily,vos.Zone as 'KaryakarZone',vos.Xetra as 'KaryakarXetra'
													FROM [dbo].[Volunteer] as Vol WITH(NOLOCK) 
													Left JOIN [dbo].[VolunteerTxn] as VolTxn  WITH(NOLOCK) ON VolTxn.VolunteerId = Vol.VolunteerId
													Left JOIN [dbo].[City] as City  WITH(NOLOCK) ON Vol.CityId = City.CityId																										
													Left JOIN [dbo].[VolunteerOtherDetail] as vod  WITH(NOLOCK) ON vod.VolunteerId = vol.VolunteerId
													Left JOIN [dbo].[VolunteerSatsang] as vos  WITH(NOLOCK) ON vos.VolunteerId = vol.VolunteerId
													Left JOIN [dbo].[Department] as Dept  WITH(NOLOCK) ON Dept.DepartmentId = VolTxn.DepartmentId													
													Where 													
													((@StartDate is null and 1=1) OR (@StartDate IS NOT NULL and 
													CAST(Vol.CreatedDate as date) between CAST(@StartDate as  date) and CAST(@EndDate as Date)))	
													AND((@DepartmentId IS NULL and 1=1) OR (@DepartmentId IS NOT NULL 
													AND Dept.DepartmentId = @DepartmentId))
													AND((@CityId IS NULL and 1=1) OR (@CityId IS NOT NULL 
													AND City.CityId = @CityId))
													Order by vol.[VolunteerId]  			                                     
													OFFSET (@PageNumber-1)*@PageSize ROWS
													FETCH NEXT @PageSize ROWS ONLY
													OPTION(RECOMPILE);";
		internal const string GetVolunteerOtherDetailsByVolunteerId = @"SELECT * from [dbo].[VolunteerOtherDetail]			                                      
													Where 
													VolunteerId=@VolunteerId";
		internal const string GetVolunteerSatsangVolunteerId = @"SELECT * from [dbo].[VolunteerSatsang]			                                      
													Where 
													VolunteerId=@VolunteerId";
	}
}
