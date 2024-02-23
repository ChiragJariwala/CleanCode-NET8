//using CleanCode.Core.Dapper;
//using CleanCode.Core.Entities;
//using CleanCode.Core.Entities.Base;
//using CleanCode.Core.Models;
//using CleanCode.Core.Repositories.Base;
//using CleanCode.Core.Specifications.Base;
//using Dapper;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System.Data;
//using System.Linq.Expressions;

//namespace CleanCode.Infrastructure.Repositories.Base
//{
//    public class Repository<T> : IRepository<T> where T : BaseEntity
//    {
//        private readonly CleanCodeContext _dbContext;
//        private readonly IConfiguration _configuration;
//        private readonly IDapper _dapper;

//        private string Connectionstring = "ConnectionString";

//        public Repository(CleanCodeContext dbContext, IConfiguration configuration)
//        {
//            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
//            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
//        }
//        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
//        {
//            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
//        }

//        public async Task<IReadOnlyList<T>> GetAllAsync()
//        {
//            return await _dbContext.Set<T>().ToListAsync();
//        }

//        public async Task<PagedList<T>> GetAsync(PaginationQuery paginationQuery)
//        {
//            // _dbContext.Database.ExecuteSqlRaw("CreateStudents @p0, @p1", parameters: new[] { "Bill", "Gates" });
//            dynamic pagedData = await _dbContext.Set<T>()
//                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
//                .Take(paginationQuery.PageSize)
//                .ToListAsync();

//            var totalRecords = paginationQuery.IncludeTotalCount ? await _dbContext.Set<T>().CountAsync() : 0;
//            return new PagedList<T>(pagedData, totalRecords, paginationQuery.PageNumber, paginationQuery.PageSize, paginationQuery.Sortby, paginationQuery.SortOrder,
//                paginationQuery.Filterby, paginationQuery.FilterValue);
//        }

//        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
//        {
//            return await ApplySpecification(spec).ToListAsync();
//        }

//        public async Task<int> CountAsync(ISpecification<T> spec)
//        {
//            return await ApplySpecification(spec).CountAsync();
//        }

//        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> condition)
//        {
//            return await _dbContext.Set<T>().Where(condition).ToListAsync();
//        }

//        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> condition,
//            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string includeString = null, bool disableTracking = true)
//        {
//            IQueryable<T> query = _dbContext.Set<T>();
//            if (disableTracking) query = query.AsNoTracking();

//            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

//            if (condition != null) query = query.Where(condition);

//            if (orderBy != null)
//                return await orderBy(query).ToListAsync();
//            return await query.ToListAsync();
//        }

//        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> condition,
//            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, List<Expression<Func<T, object>>> includes = null,
//            bool disableTracking = true)
//        {
//            IQueryable<T> query = _dbContext.Set<T>();
//            if (disableTracking) query = query.AsNoTracking();

//            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

//            if (condition != null) query = query.Where(condition);

//            if (orderBy != null)
//                return await orderBy(query).ToListAsync();
//            return await query.ToListAsync();
//        }

//        public virtual async Task<T> GetByIdAsync(int id)
//        {
//            if (id <= 0)
//                return null;
//            return await _dbContext.Set<T>().FindAsync(id);
//        }

//        public async Task<T> AddAsync(T entity)
//        {
//            try
//            {
//                await _dbContext.Set<T>().AddAsync(entity);
//                await _dbContext.SaveChangesAsync();
//                return entity;
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public async Task UpdateAsync(T entity)
//        {
//            _dbContext.Entry(entity).State = EntityState.Modified;
//            await _dbContext.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(T entity)
//        {
//            _dbContext.Set<T>().Remove(entity);
//            await _dbContext.SaveChangesAsync();
//        }

//        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null,
//            IDbTransaction transaction = null, CancellationToken cancellationToken = default)
//        {
//            await using var connection = new SqlConnection(_configuration.GetConnectionString(Connectionstring));
//            await connection.OpenAsync(cancellationToken);
//            return (await connection.QueryAsync<T>(sql, param, transaction)).ToList();
//        }

//        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null,
//            IDbTransaction transaction = null, CancellationToken cancellationToken = default)
//        {
//            await using var connection = new SqlConnection(_configuration.GetConnectionString(Connectionstring));
//            await connection.OpenAsync(cancellationToken);
//            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
//        }

//        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
//            CancellationToken cancellationToken = default)
//        {
//            await using var connection = new SqlConnection(_configuration.GetConnectionString(Connectionstring));
//            await connection.OpenAsync(cancellationToken);
//            return await connection.QuerySingleAsync<T>(sql, param, transaction);
//        }

//        public async Task<int> ExecuteAsync<T>(string sql, object param = null, IDbTransaction transaction = null,
//            CancellationToken cancellationToken = default)
//        {
//            await using var connection = new SqlConnection(_configuration.GetConnectionString(Connectionstring));
//            await connection.OpenAsync(cancellationToken);
//            return await connection.ExecuteAsync(sql, param, transaction);
//        }
//    }
//}
