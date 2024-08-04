using Appointments.Data.Entities;

namespace Appointments.Data.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Client> GetClientById(int clientId);
        Task<Client> AddClient(Client client);
        Task<Client> UpdateClient(Client client, int clientId);
        Task<bool> DeleteClient(int clientId);
    }
}
