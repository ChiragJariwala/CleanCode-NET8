using CleanCode.Business.Models;
using CleanCode.Core.Dapper;
using CleanCode.Core.Entities;
using CleanCode.Core.Models;
using CleanCode.Core.Repositories;
using CleanCode.Infrastructure.Repositories.Base;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Infrastructure.Repositories
{
    public class VolunteerRepository : Repository<GetVolunteerList>, IVolunteerRepository
    {
        private readonly CleanCodeContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductRepository> _logger;
        private readonly IDapper _dapper;
        public VolunteerRepository(CleanCodeContext dbContext, IConfiguration configuration, ILogger<ProductRepository> logger, IDapper dapper)
           : base(dbContext, configuration)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
        }

        public async Task<PagedList<GetVolunteerList>> GetVolunteerList(VolunteerFilter volunteerFilter)
        {
            var skillHobbyMetaData = await _dapper.GetAllAsync<GetVolunteerList>(SQLQueries.GetVolunteerList,
                new DynamicParameters(new
                {                  
                    DepartmentId = volunteerFilter.DepartmentId,
                    CityId = volunteerFilter.CityId,
                    PageNumber = volunteerFilter.PageNumber,
                    PageSize = volunteerFilter.PageSize,
                    StartDate = volunteerFilter.StartDate,
                    EndDate = volunteerFilter.EndDate
                }),
                CommandType.Text).ConfigureAwait(false);
            // var pagedData = skillHobbyMetaData.Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize).Take(paginationQuery.PageSize).ToList();
            // var totalRecords = paginationQuery.IncludeTotalCount ? pagedData.Count : 0;
            // Option 6: Standard EFCore Way
            /* var pagedData = await _dbContext.Products
                .OrderBy(x => x.ProductId)
                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync();
           var totalRecords = paginationQuery.IncludeTotalCount ? await _dbContext.Products.CountAsync() : 0;*/
            return new PagedList<GetVolunteerList>(skillHobbyMetaData, skillHobbyMetaData.Count(), volunteerFilter.PageNumber, volunteerFilter.PageSize);
        }

        public List<VolunteerSkills> GetVolunteerSkillsList(int VolunteerId)
        {
            var skillHobbyMetaData = _dapper.GetAll<VolunteerSkills>(SQLQueries.GetVolunteerSkillsList,
                new DynamicParameters(new
                {
                    VolunteerId = VolunteerId
                }),
                CommandType.Text);           
            // Option 6: Standard EFCore Way
            /* var pagedData = await _dbContext.Products
                .OrderBy(x => x.ProductId)
                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync();
           var totalRecords = paginationQuery.IncludeTotalCount ? await _dbContext.Products.CountAsync() : 0;*/
            return skillHobbyMetaData;
        }

        public List<VolunteerComputerSkills> GetVolunteerComputerSkillsList(int VolunteerId)
        {
            var skillHobbyMetaData = _dapper.GetAll<VolunteerComputerSkills>(SQLQueries.GetVolunteerComputerSkillsList,
                new DynamicParameters(new
                {
                    VolunteerId = VolunteerId
                }),
                CommandType.Text);
            // Option 6: Standard EFCore Way
            /* var pagedData = await _dbContext.Products
                .OrderBy(x => x.ProductId)
                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync();
           var totalRecords = paginationQuery.IncludeTotalCount ? await _dbContext.Products.CountAsync() : 0;*/
            return skillHobbyMetaData;
        }


        public async Task<GetVolunteerList> GetVolunteerByMobileNo(string mobileNo)
        {
            var volunteerData = await _dapper.GetAllAsync<GetVolunteerList>(SQLQueries.SelectVolunteerByMobileNo,
                new DynamicParameters(new
                {
                    MobileNo = mobileNo
                }),
                CommandType.Text).ConfigureAwait(false);

            if (volunteerData.Count() > 0)
                return volunteerData[0];
            else
                return null;
        }

        public GetVolunteerList GetVolunteerByVolunteerId(int VolunteerId)
        {
            var volunteerData = _dapper.Get<GetVolunteerList>(SQLQueries.GetVolunteerById,
               new DynamicParameters(new
               {
                   VolunteerId = VolunteerId
               }),
               CommandType.Text);
            return volunteerData;
        }

        public VolunteerOtherDetail GetVolunteerOtherDetailsByVolunteerId(int id)
        {
            var volunteerOtherData = _dapper.Get<VolunteerOtherDetail>(SQLQueries.GetVolunteerOtherDetailsByVolunteerId,
               new DynamicParameters(new
               {
                   VolunteerId = id
               }),
               CommandType.Text);
            return volunteerOtherData;
        }

        public VolunteerSatsang GetVolunteerSatsangByVolunteerId(int id)
        {
            var volunteerOtherData = _dapper.Get<VolunteerSatsang>(SQLQueries.GetVolunteerSatsangVolunteerId,
             new DynamicParameters(new
             {
                 VolunteerId = id
             }),
             CommandType.Text);
            return volunteerOtherData;
        }

        public async Task<IEnumerable<GetVolunteerDropdown>> GetVolunteerDropdown(VolunteerFilter volunteerFilter)
        {
            var skillHobbyMetaData = await _dapper.GetAllAsync<GetVolunteerDropdown>(SQLQueries.GetVolunteerDropDownList,
                new DynamicParameters(),
                CommandType.Text).ConfigureAwait(false);
            // var pagedData = skillHobbyMetaData.Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize).Take(paginationQuery.PageSize).ToList();
            // var totalRecords = paginationQuery.IncludeTotalCount ? pagedData.Count : 0;
            // Option 6: Standard EFCore Way
            /* var pagedData = await _dbContext.Products
                .OrderBy(x => x.ProductId)
                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync();
           var totalRecords = paginationQuery.IncludeTotalCount ? await _dbContext.Products.CountAsync() : 0;*/
            return skillHobbyMetaData;
        }
    }
}
