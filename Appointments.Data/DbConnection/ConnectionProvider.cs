using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Appointments.Data.DbConnection
{
    public class ConnectionProvider
    {
        private readonly string _connectionString;
        private readonly ILogger<ConnectionProvider> _logger;

        public ConnectionProvider(string connectionString, ILogger<ConnectionProvider> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string storedProcedure, object parameters = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error ocurred executing {storedProcedure}");
                throw;
            }
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string storedProcedure, object parameters = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    return await connection.QuerySingleOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred executing {storedProcedure}");
                throw;
            }
        }

        public async Task<int> ExecuteAsync(string storedProcedure, object parameters = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    return await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred executing {storedProcedure}");
                throw;
            }
        }
    }
}
