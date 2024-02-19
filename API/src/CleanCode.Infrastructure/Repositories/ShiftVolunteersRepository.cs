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
    public class ShiftVolunteersRepository : Repository<VolunteersByShift>, IShiftVolunteersRepository
    {        
        private readonly CleanCodeContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ShiftVolunteersRepository> _logger;
        private readonly IDapper _dapper;
        public ShiftVolunteersRepository(CleanCodeContext dbContext, IConfiguration configuration, ILogger<ShiftVolunteersRepository> logger, IDapper dapper)
           : base(dbContext, configuration)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
        }

        public Task<PagedList<VolunteersByShift>> Get(PaginationQuery paginationQuery)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VolunteersByShift>> GetVolunteersByShiftId(PaginationQuery paginationQuery,int shiftId)
        {

            var param = new Dictionary<string, object>
            {
                { "@ShiftId",shiftId }
            };            
            var skillHobbyMetaData = await _dapper.GetAllAsync<VolunteersByShift>(SQLQueries.SelectShiftVolunteersByShiftId, new DynamicParameters(param), CommandType.Text).ConfigureAwait(false);
            var pagedData = skillHobbyMetaData.Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize).Take(paginationQuery.PageSize).ToList(); ;
            var totalRecords = paginationQuery.IncludeTotalCount ? pagedData.Count : 0;
            // Option 6: Standard EFCore Way
            /*var pagedData = await _dbContext.Products
                .OrderBy(x => x.ProductId)
                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync();
            var totalRecords = paginationQuery.IncludeTotalCount ? await _dbContext.Products.CountAsync() : 0;*/

            return new PagedList<VolunteersByShift>(pagedData, totalRecords, paginationQuery.PageNumber, paginationQuery.PageSize);
            //Comeented code because it was with sample databse. now we are using main DB
            //    return await _dbContext.Products
            //        .Where(x => x.CategoryId == categoryId)
            //        .ToListAsync();            
        }

        public async Task<IEnumerable<ShiftsByVolunteer>> GetShiftsByVolunteerId(PaginationQuery paginationQuery, int VolunteerId)
        {

            var param = new Dictionary<string, object>
            {
                { "@VolunteerId",VolunteerId }
            };
            var skillHobbyMetaData = await _dapper.GetAllAsync<ShiftsByVolunteer>(SQLQueries.SelectShiftVolunteersByVolunteerId, new DynamicParameters(param), CommandType.Text).ConfigureAwait(false);
            var pagedData = skillHobbyMetaData.Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize).Take(paginationQuery.PageSize).ToList(); ;
            var totalRecords = paginationQuery.IncludeTotalCount ? pagedData.Count : 0;
            // Option 6: Standard EFCore Way
            /*var pagedData = await _dbContext.Products
                .OrderBy(x => x.ProductId)
                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync();
            var totalRecords = paginationQuery.IncludeTotalCount ? await _dbContext.Products.CountAsync() : 0;*/

            return new PagedList<ShiftsByVolunteer>(pagedData, totalRecords, paginationQuery.PageNumber, paginationQuery.PageSize);
            //Comeented code because it was with sample databse. now we are using main DB
            //    return await _dbContext.Products
            //        .Where(x => x.CategoryId == categoryId)
            //        .ToListAsync();            
        }
    }
}
