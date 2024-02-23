@ECHO OFF
ECHO ...... dbcontext scaffold Command _start.....
ECHO ...... Pointing to project BNMS.Core ...... 
cd ..
echo %CD% 
cd CleanCode.Core
echo %CD% 
dotnet ef dbcontext scaffold "Server=.;Database=Northwind;persist security info=True;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -p CleanCode.Core.csproj -o Entities -c CleanCodeContext -d -f
ECHO ...... dbcontext scaffold Command completed .....

ECHO ...... Run CleanCode.Tools exe ...... 
cd ..
cd CleanCode.Tools\bin\Debug\net8.0\
CleanCode.Tools.exe false false

ECHO ...... MsBuild started...... 
CD C:\Windows\Microsoft.NET\Framework64\v4.0.30319
msbuild "D:\BAPS\Clean Code Ddemo\CleanCodeDemo\API\CleanCodeDemo.sln" /p:configuration=debug
echo ...... build complete ...... 
pause