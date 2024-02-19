using CleanCode.Core.Dapper;
using CleanCode.Core.Entities;
using CleanCode.Core.Models;
using CleanCode.Core.Repositories;
using CleanCode.Infrastructure.Repositories.Base;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanCode.Infrastructure.Repositories
{
    public class MetadataRepository : Repository<Metadata>, IMetadataRepository
    {
        private readonly CleanCodeContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MetadataRepository> _logger;
        private readonly IDapper _dapper;
        public MetadataRepository(CleanCodeContext dbContext, IConfiguration configuration, ILogger<MetadataRepository> logger, IDapper dapper)
           : base(dbContext, configuration)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
        }
        public async Task<List<object>> Get(string tableName)
        {
            // Option 5: Using Repository Generic Method with QueryAsync (Dapper)
            var metadataList = await _dapper.GetAllAsync<Object>("select * from " + tableName, new DynamicParameters(new { }), CommandType.Text).ConfigureAwait(false);
            return new List<object>(metadataList);
        }

        public async Task<List<object>> Get(string tableName, string filtercolumn, string filterValue)
        {
            dynamic metadataList;
            string sWherecondition = "where " + filtercolumn + "=" + filterValue;
            try
            {
                metadataList = await _dapper.GetAllAsync<Object>("select * from " + tableName + " " + sWherecondition, new DynamicParameters(new { }), CommandType.Text).ConfigureAwait(false);
            }
            catch
            {
                sWherecondition = "where " + filtercolumn + "='" + filterValue + "'";
                metadataList = await _dapper.GetAllAsync<Object>("select * from " + tableName + " " + sWherecondition, new DynamicParameters(new { }), CommandType.Text).ConfigureAwait(false);
            }
            return new List<object>(metadataList);
        }

        public async Task<List<object>> Get(string tableName, string whereCondition)
        {
            string sWherecondition = "where " + whereCondition;
            dynamic metadataList = await _dapper.GetAllAsync<Object>("select * from " + tableName + " " + sWherecondition, new DynamicParameters(new { }), CommandType.Text).ConfigureAwait(false);
            return new List<object>(metadataList);
        }
    }
}