using Appointments.Data.Repositories.Interfaces;
using Appointments.Domain.DTOs;
using Appointments.Domain.Services.Interfaces;
using Appointments.Domain.Mappers;
using Microsoft.Extensions.Logging;

namespace Appointments.Domain.Services
{
    public class ServiceTypesService : IServiceTypesService
    {
        private readonly ILogger<ServiceTypesService> _logger;
        private readonly IServiceTypesRepository _serviceTypesRepository;

        public ServiceTypesService(ILogger<ServiceTypesService> logger, IServiceTypesRepository serviceTypesRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceTypesRepository = serviceTypesRepository ?? throw new ArgumentNullException(nameof(serviceTypesRepository));
        }

        public async Task<IEnumerable<ServiceTypeDTO>> GetServiceTypesAsync()
        {
            _logger.LogInformation("Starting call repository to getting all service types");
            var response = await _serviceTypesRepository.GetServiceTypes().ConfigureAwait(false);

            if (response is null)
            {
                _logger.LogError("No service types found");
                return Enumerable.Empty<ServiceTypeDTO>();
            }

            var mappedResponse = response.Select(Mapper.MapToServiceTypeDTO);
            _logger.LogInformation("Service types found successfully");
            return mappedResponse;
        }

        public Task<ServiceTypeDTO> GetServiceTypeByIdAsync(int serviceTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceTypeDTO> AddServiceTypeAsync(ServiceTypeDTO serviceType)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceTypeDTO> UpdateServiceTypeAsync(ServiceTypeDTO serviceType, int serviceTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteServiceTypeAsync(int serviceTypeId)
        {
            throw new NotImplementedException();
        }

        #region SUPPORT METHODS
        
        #endregion
    }
}
