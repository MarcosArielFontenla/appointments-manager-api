using Appointments.Domain.DTOs;

namespace Appointments.Domain.Services.Interfaces
{
    public interface IServiceTypesService
    {
        Task<IEnumerable<ServiceTypeResponse>> GetServiceTypesAsync();
        Task<ServiceTypeResponse> GetServiceTypeByIdAsync(int serviceTypeId);
        Task<ServiceTypeResponse> AddServiceTypeAsync(ServiceTypeResponse serviceType);
        Task<ServiceTypeResponse> UpdateServiceTypeAsync(ServiceTypeResponse serviceType, int serviceTypeId);
        Task<bool> DeleteServiceTypeAsync(int serviceTypeId);
    }
}
