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

        public async Task<IEnumerable<ServiceTypeResponse>> GetServiceTypesAsync()
        {
            _logger.LogInformation("Starting call repository to getting all service types");
            var response = await _serviceTypesRepository.GetServiceTypes().ConfigureAwait(false);

            if (response is null)
            {
                _logger.LogError("No service types found");
                return Enumerable.Empty<ServiceTypeResponse>();
            }

            var mappedResponse = response.Select(Mapper.MapToServiceTypeDTO);
            _logger.LogInformation("Service types found successfully");
            return mappedResponse;
        }

        public async Task<ServiceTypeResponse> GetServiceTypeByIdAsync(int serviceTypeId)
        {
            _logger.LogInformation("Starting call repository to getting service type by id");
            var response = await _serviceTypesRepository.GetServiceTypeById(serviceTypeId).ConfigureAwait(false);

            if (response is null)
            {
                _logger.LogError("No service type found");
                return null;
            }

            var mappedResponse = Mapper.MapToServiceTypeDTO(response);
            _logger.LogInformation("Service type found successfully");
            return mappedResponse;
        }

        public async Task<ServiceTypeResponse> AddServiceTypeAsync(ServiceTypeResponse serviceType)
        {
            _logger.LogInformation("Starting call repository to add service type");
            var mappedServiceType = Mapper.MapServiceTypesResponseToServiceType(serviceType);
            var response = await _serviceTypesRepository.AddServiceType(mappedServiceType).ConfigureAwait(false);

            if (response is null) 
            {
                _logger.LogError("Service type not added");
                return null;
            }
            var mappedResponse = Mapper.MapToServiceTypeDTO(response);
            _logger.LogInformation("Service type added successfully");
            return mappedResponse;

        }

        public Task<ServiceTypeResponse> UpdateServiceTypeAsync(ServiceTypeResponse serviceType, int serviceTypeId)
        {
            _logger.LogInformation("Starting call repository to update service type");
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
