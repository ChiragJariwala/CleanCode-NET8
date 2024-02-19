@ECHO OFF
ECHO ...... dbcontext scaffold Command _start.....
ECHO ...... Pointing to project BNMS.Core ...... 
cd ..
echo %CD% 
cd BNMS.Core
echo %CD% 
dotnet ef dbcontext scaffold "Server=103.92.235.45;Database=stihydra_balnagari;persist security info=True;user id=stihydra_bal;password=bapa@100#;" Microsoft.EntityFrameworkCore.SqlServer -p BNMS.Core.csproj -o Entities -c BNMSContext -t Role -t User -t VolunteerTxn -t Volunteer -t VolunteerSkill -t BillBatch -t BillEntry -t Designation -t Department -t Vehicle -t VehicleAvailability -t FuelCostLog -t VehicleCategory -t FuelType -t KilometerLog -t VehicleAllottedToDriver -t VehicleRequest -t VehicleRequestApproval -t VehicleBooking -t VehicleTrip -t JourneyStatus -t vw_ActiveDriverList -t vw_VehicleNotAllottedDriverList -t LoadTestVolunteer -t Request -t RequestType -t RequestStatus -t DepartmentLocation -t Location -t SubLocation -t Shift -t ShiftVolunteer -t VolunteerSatsang -t VolunteerOtherDetail -t Skill -t ComputerSkill -t SubDepartment -d -f
ECHO ...... dbcontext scaffold Command completed .....

ECHO ...... Run BNMS.Tools exe ...... 
cd ..
cd BNMS.Tools\bin\Debug\net6.0\
BNMS.Tools.exe false false

ECHO ...... MsBuild started...... 
CD C:\Windows\Microsoft.NET\Framework64\v4.0.30319
msbuild "C:\Bhavesh\BPCO\BNMS_Git\BNMS_API\BNMS.sln" /p:configuration=debug
echo ...... build complete ...... 
pause