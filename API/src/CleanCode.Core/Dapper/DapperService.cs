using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CleanCode.Core.Dapper
{
    public class DapperService : IDapper
    {
        private readonly IConfiguration _config;
        private readonly SqlConnection _dbConnection;

        public DapperService(IConfiguration config, SqlConnection dbConnection)
        {
            _config = config;
            _dbConnection = dbConnection;
        }
        public void Dispose()
        {

        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            //throw new NotImplementedException();
            //return _dbConnection.Execute(sp+ "; SELECT @ID = SCOPE_IDENTITY()", parms, commandType: commandType) ;
            return _dbConnection.Execute(sp, parms, commandType: commandType);
        }
        public dynamic QueryFirstOrDefault(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            //throw new NotImplementedException();
            //return _dbConnection.Execute(sp+ "; SELECT @ID = SCOPE_IDENTITY()", parms, commandType: commandType) ;
            return _dbConnection.QueryFirstOrDefault(sp, parms, commandType: commandType);
        }

        public object ExecuteScaler(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            //throw new NotImplementedException();
            //return _dbConnection.Execute(sp+ "; SELECT @ID = SCOPE_IDENTITY()", parms, commandType: commandType) ;
            return _dbConnection.ExecuteScalar(sp, parms, commandType: commandType);
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            return _dbConnection.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        public async Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            var result = await _dbConnection.QueryAsync<T>(sp, parms, commandType: commandType);
            return result.FirstOrDefault();

        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            return _dbConnection.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public async Task<List<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {       var result = await _dbConnection.QueryAsync<T>(sp, parms, commandType: commandType);
                return result.ToList();                
        }
        public async Task<List<T>> QueryMultipleAsync<T>(string sp, DynamicParameters parms, CommandType commandType)
        {
          var result = await _dbConnection.QueryMultipleAsync(sp, parms, null, null, commandType: commandType);
          var responseTable = (await result.ReadAsync<T>()).ToList();
          return responseTable.ToList();
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            result = _dbConnection.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();

            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            result = _dbConnection.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();

            return result;
        }

        public async Task<int> ExecuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            return await _dbConnection.ExecuteAsync(sp, parms, null, null, commandType: commandType);
        }

        public async Task<int> ExecuteScalerAsync(string sp, DynamicParameters parms, CommandType commandType)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(sp, parms, null, null, commandType: commandType);
        }
        public async Task<int> ExecuteAsync(string sp, List<DynamicParameters> parms, CommandType commandType = CommandType.Text)
        {
            return await _dbConnection.ExecuteAsync(sp, parms, null, null, commandType: commandType);
        }
    }
}
