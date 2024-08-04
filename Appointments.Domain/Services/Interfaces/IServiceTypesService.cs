using Appointments.Domain.DTOs;

namespace Appointments.Domain.Services.Interfaces
{
    public interface IServiceTypesService
    {
        Task<IEnumerable<ServiceTypeDTO>> GetServiceTypesAsync();
        Task<ServiceTypeDTO> GetServiceTypeByIdAsync(int serviceTypeId);
        Task<ServiceTypeDTO> AddServiceTypeAsync(ServiceTypeDTO serviceType);
        Task<ServiceTypeDTO> UpdateServiceTypeAsync(ServiceTypeDTO serviceType, int serviceTypeId);
        Task<bool> DeleteServiceTypeAsync(int serviceTypeId);
    }
}
