using Appointments.Data.Entities;
using Appointments.Domain.DTOs;

namespace Appointments.Domain.Mappers
{
    public static class Mapper
    {
        public static ServiceTypeResponse MapToServiceTypeDTO(ServiceType serviceType)
        {
            return new ServiceTypeResponse
            {
                ServiceTypeId = serviceType.ServiceTypeId,
                Name = serviceType.Name,
                Description = serviceType.Description,
                Price = serviceType.Price
            };
        }

        public static ServiceType MapServiceTypesResponseToServiceType(ServiceTypeResponse serviceType)
        {
            return new ServiceType
            {
                ServiceTypeId = serviceType.ServiceTypeId,
                Name = serviceType.Name,
                Description = serviceType.Description,
                Price = serviceType.Price
            };
        }

        public static ClientResponse MapClientToClientResponse(Client client)
        {
            return new ClientResponse
            {
                ClientId = client.ClientId,
                Name = client.Name,
                LastName = client.LastName,
                Phone = client.Phone,
                Email = client.Email,
                StartDate = client.StartDate
            };
        }
    }
}
