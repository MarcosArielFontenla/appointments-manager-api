using Appointments.Data.Entities;

namespace Appointments.Data.Repositories.Interfaces
{
    public interface IServiceTypesRepository
    {
        Task<IEnumerable<ServiceType>> GetServiceTypes();
        Task<ServiceType> GetServiceTypeById(int serviceTypeId);
        Task<ServiceType> AddServiceType(ServiceType serviceType);
        Task<ServiceType> UpdateServiceType(ServiceType serviceType, int serviceTypeId);
        Task<bool> DeleteServiceType(int serviceTypeId);
    }
}
