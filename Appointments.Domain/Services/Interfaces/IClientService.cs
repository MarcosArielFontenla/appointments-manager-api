using Appointments.Data.Entities;
using Appointments.Domain.DTOs;

namespace Appointments.Domain.Services.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientResponse>> GetClientsAsync();
        Task<ClientResponse> GetClientByIdAsync(int clientId);
        Task<ClientResponse> AddClientAsync(Client client);
        Task<ClientResponse> UpdateClientAsync(Client client, int clientId);
        Task<bool> DeleteClientAsync(int clientId);
    }
}
