using CleanCode.Core.Dapper;
using CleanCode.Core.Entities;
using CleanCode.Core.Models;
using CleanCode.Core.Repositories;
using CleanCode.Infrastructure.Repositories.Base;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CleanCode.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly CleanCodeContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductRepository> _logger;
        private readonly IDapper _dapper;
        public ProductRepository(CleanCodeContext dbContext, IConfiguration configuration, ILogger<ProductRepository> logger, IDapper dapper)
           : base(dbContext, configuration)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
        }
        public async Task<PagedList<Product>> Get(PaginationQuery paginationQuery)
        {
            // Option 1: Using Specification & Generic Repository (EFCore)
            //var spec = new ProductSpecification();
            //return await GetAsync(spec);

            // Option 2: Using Repository Generic Method - GetAllAsync (EFCore)
            //return await GetAllAsync();

            // Option 3: Using Repository Generic Method with Category Include and Disable Tracking - GetAsync Overloaded Method (EFCore)
            //return await GetAsync(null, null, "Category", true);

            // Option 4: Using Repository Generic Method with Category Include, Disable Tracking & Order By Product Name - GetAsync Overloaded Method (EFCore)
            //return await GetAsync(null, o => o.OrderBy(s => s.Name), "Category", true);

            // Option 5: Using Raw SQL (EFCore)
            //return await _dbContext.Products.FromSqlRaw("SELECT ProductId, Name, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, CategoryId FROM PRODUCT").ToListAsync();

            // Option 5: Using Repository Generic Method with QueryAsync (Dapper)
            var skillHobbyMetaData = await _dapper.GetAllAsync<Product>(SQLQueries.SelectPersonSkillHobbyByPersonSkillId, new DynamicParameters(new { }), CommandType.Text).ConfigureAwait(false);
            var pagedData = skillHobbyMetaData.Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize).Take(paginationQuery.PageSize).ToList(); ;
            var totalRecords = paginationQuery.IncludeTotalCount ? pagedData.Count : 0;
            // Option 6: Standard EFCore Way
            /*var pagedData = await _dbContext.Products
                .OrderBy(x => x.ProductId)
                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .ToListAsync();
            var totalRecords = paginationQuery.IncludeTotalCount ? await _dbContext.Products.CountAsync() : 0;*/
            return new PagedList<Product>(pagedData, totalRecords, paginationQuery.PageNumber, paginationQuery.PageSize);
        }
        public Task<IEnumerable<Product>> GetByCategoryId(int categoryId)
        {
         //Comeented code because it was with sample databse. now we are using main DB
         //    return await _dbContext.Products
         //        .Where(x => x.CategoryId == categoryId)
         //        .ToListAsync();
         return null;
        }
    }
}