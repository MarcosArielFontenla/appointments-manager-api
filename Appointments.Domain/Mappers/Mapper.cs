using Appointments.Data.Entities;
using Appointments.Domain.DTOs;

namespace Appointments.Domain.Mappers
{
    public static class Mapper
    {
        public static ServiceTypeDTO MapToServiceTypeDTO(ServiceType serviceType)
        {
            return new ServiceTypeDTO
            {
                ServiceTypeId = serviceType.ServiceTypeId,
                Name = serviceType.Name,
                Description = serviceType.Description,
                Price = serviceType.Price
            };
        }
    }
}
