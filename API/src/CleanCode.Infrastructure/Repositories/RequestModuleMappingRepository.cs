using CleanCode.Core.Dapper;
using CleanCode.Core.Entities;
using CleanCode.Core.Repositories;
using CleanCode.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CleanCode.Infrastructure.Repositories
{
    public class RequestModuleMappingRepository : Repository<RequestModuleMapping>, IRequestModuleMappingRepository
    {
        private readonly CleanCodeContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RequestModuleMappingRepository> _logger;
        private readonly IDapper _dapper;
        public RequestModuleMappingRepository(CleanCodeContext dbContext, IConfiguration configuration, ILogger<RequestModuleMappingRepository> logger, IDapper dapper) : base(dbContext, configuration)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
        }

        public async Task<IEnumerable<RequestType>> GetRequestTypeByDepartmentId(int departmentId)
        {
            throw new NotImplementedException();
            //return await _dbContext.RequestModuleMappings
            //            .Join(
            //                _dbContext.RequestTypes,
            //                requestModuleMapping => requestModuleMapping.ReuestTypeId,
            //                requestType => requestType.RequestTypeId,
            //                (requestModuleMapping,requestType) => new RequestType
            //                {
            //                    RequestTypeId = requestType.RequestTypeId,
            //                    RequestTypeName = requestType.RequestTypeName,
            //                    DepartmentId = requestModuleMapping.DepartmentId
            //                }
            //               )
            //       .Where(x => x.DepartmentId == departmentId)
            //       .ToListAsync();
        }
    }
}
