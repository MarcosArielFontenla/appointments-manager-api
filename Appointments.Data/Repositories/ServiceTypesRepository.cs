using Appointments.Data.DbConnection;
using Appointments.Data.Entities;
using Appointments.Data.Helpers;
using Appointments.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation("Starting call repository to getting all service types");

            try
            {
                return await _connectionProvider.QueryAsync<ServiceType>(ServiceTypesHelper.SP_GET_SERVICE_TYPES);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all service types");
                throw;
            }
        }

        public async Task<ServiceType> GetServiceTypeById(int serviceTypeId)
        {
            _logger.LogInformation("Starting call repository to getting service type by id");

            try
            {
                return await _connectionProvider.QuerySingleOrDefaultAsync<ServiceType>(
                ServiceTypesHelper.SP_GET_SERVICE_TYPE_BY_ID,
                new { ServiceTypeId = serviceTypeId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting service type by id");
                throw;
            }
        }

        public async Task<ServiceType> AddServiceType(ServiceType serviceType)
        {
            var parameters = new
            {
                serviceType.Name,
                serviceType.Description,
                serviceType.Price
            };
            _logger.LogInformation("Starting call repository to add service type");

            try
            {
                var newId = await _connectionProvider.QuerySingleOrDefaultAsync<int>(ServiceTypesHelper.SP_ADD_SERVICE_TYPE, parameters);
                serviceType.ServiceTypeId = newId;
                _logger.LogInformation("Service type added successfully");
                return serviceType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding service type");
                throw;
            }
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
            _logger.LogInformation("Starting call repository to update service type");

            try
            {
                await _connectionProvider.ExecuteAsync(ServiceTypesHelper.SP_UPDATE_SERVICE_TYPE, parameters);
                _logger.LogInformation("Service type updated successfully");
                return serviceType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating service type");
                throw;
            }
        }

        public async Task<bool> DeleteServiceType(int serviceTypeId)
        {
            _logger.LogInformation("Starting call repository to delete service type");

            try
            {
                var rowsAffected = await _connectionProvider.ExecuteAsync(
                ServiceTypesHelper.SP_DELETE_SERVICE_TYPE,
                new { ServiceTypeId = serviceTypeId });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting service type");
                throw;
            }
        }
    }
}
