using Appointments.Data.DbConnection;
using Appointments.Data.Entities;
using Appointments.Data.Helpers;
using Appointments.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Appointments.Data.Repositories
{
    public class ServiceTypesRepository : IServiceTypesRepository
    {
        private readonly ILogger<ServiceTypesRepository> _logger;
        private readonly ConnectionProvider _connectionProvider;

        public ServiceTypesRepository(ILogger<ServiceTypesRepository> logger, ConnectionProvider connectionProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
        }

        public async Task<IEnumerable<ServiceType>> GetServiceTypes()
        {
            return await _connectionProvider.QueryAsync<ServiceType>(ServiceTypesHelper.SP_GET_SERVICE_TYPES);
        }

        public async Task<ServiceType> GetServiceTypeById(int serviceTypeId)
        {
            return await _connectionProvider.QuerySingleOrDefaultAsync<ServiceType>(
                ServiceTypesHelper.SP_GET_SERVICE_TYPE_BY_ID, 
                new { ServiceTypeId = serviceTypeId });
        }

        public async Task<ServiceType> AddServiceType(ServiceType serviceType)
        {
            var parameters = new
            {
                serviceType.Name,
                serviceType.Description,
                serviceType.Price
            };

            var newId = await _connectionProvider.QuerySingleOrDefaultAsync<int>(ServiceTypesHelper.SP_ADD_SERVICE_TYPE, parameters);

            serviceType.ServiceTypeId = newId;
            return serviceType;
        }

        public async Task<ServiceType> UpdateServiceType(ServiceType serviceType, int serviceTypeId)
        {
            var parameters = new
            {
                ServiceTypeId = serviceTypeId,
                serviceType.Name,
                serviceType.Description,
                serviceType.Price
            };

            await _connectionProvider.ExecuteAsync(ServiceTypesHelper.SP_UPDATE_SERVICE_TYPE, parameters);
            return serviceType;
        }

        public async Task<bool> DeleteServiceType(int serviceTypeId)
        {
            var rowsAffected = await _connectionProvider.ExecuteAsync(
                ServiceTypesHelper.SP_DELETE_SERVICE_TYPE, 
                new { ServiceTypeId = serviceTypeId });

            return rowsAffected > 0;
        }
    }
}
