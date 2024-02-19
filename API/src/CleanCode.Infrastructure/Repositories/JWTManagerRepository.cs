using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CleanCode.Core.Dapper;
using CleanCode.Core.Entities;
using CleanCode.Core.Models;
using CleanCode.Core.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CleanCode.Infrastructure.Repositories
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration iconfiguration;
        public JWTManagerRepository(IConfiguration iconfiguration, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            this.iconfiguration = iconfiguration;
        }
        public Tokens Authenticate(Users users)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@MobileNo", users.mobileno);
            dynamicParameters.Add("@Password", users.Password);

            using (var scope = _serviceProvider.CreateScope())
            {
                var _dapper = scope.ServiceProvider.GetRequiredService<IDapper>();
                // do something with context

                var usersRecords = _dapper.GetAll<User>(SQLQueries.SelectUserByCredentials, dynamicParameters, CommandType.Text);

                if (usersRecords.Count() > 0)
                {
                    var userrecord = usersRecords[0];
                    // Else we generate JSON Web Token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.MobilePhone, users.mobileno)
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                   
                    Tokens result = new Tokens { Token = tokenHandler.WriteToken(token),UserId = userrecord.UserId, Name = userrecord.FullName, MobileNo = userrecord.MobileNo, lstDepartmentDesignation = new List<UserDepartmentDesignationStatus>() };
                    dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@VolunteerId", userrecord.VolunteerId);                    
                    var usersDepartments = _dapper.GetAll<VolunteerDepartmentodel>(SQLQueries.SelectUserDepartmentDesignations, dynamicParameters, CommandType.Text);
                    foreach (VolunteerDepartmentodel vt in usersDepartments)
                    {
                        result.lstDepartmentDesignation.Add(new UserDepartmentDesignationStatus()
                        {
                            VolunteerTxnId = vt.VolunteerTxnId,
                            VolunteerId = userrecord.VolunteerId,
                            DepartmentId = vt.DepartmentId,
                            DepartmentName = vt.DepartmentName,
                            DesignationName = vt.DesignationName,
                            DesignationId = vt.DesignationId,
                            Status = vt.IsActive.HasValue ? vt.IsActive.Value : false
                        });
                    }

                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
