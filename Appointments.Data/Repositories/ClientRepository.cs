using Appointments.Data.DbConnection;
using Appointments.Data.Entities;
using Appointments.Data.Helpers;
using Appointments.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Appointments.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ILogger<ServiceTypesRepository> _logger;
        private readonly ConnectionProvider _connectionProvider;

        public ClientRepository(ILogger<ServiceTypesRepository> logger, ConnectionProvider connectionProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            _logger.LogInformation("Starting call repository to getting all clients");
            try
            {
                return await _connectionProvider.QueryAsync<Client>(ClientsHelper.SP_GET_CLIENTS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all clients");
                throw;
            }
        }

        public async Task<Client> GetClientById(int clientId)
        {
            var parameters = new
            {
                ClientId = clientId
            };

            _logger.LogInformation("Starting call repository to getting client by id");
            try
            {
                var response = await _connectionProvider.QuerySingleOrDefaultAsync<Client>(ClientsHelper.SP_GET_CLIENT_BY_ID, parameters);

                if (response is null)
                {
                    _logger.LogError("No client found");
                    return null;
                }
                _logger.LogInformation("Client found successfully");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting client by id");
                throw;
            }
        }

        public async Task<Client> AddClient(Client client)
        {
            var parameters = new
            {
                client.Name,
                client.LastName,
                client.Phone,
                client.Email,
                client.StartDate
            };

            _logger.LogInformation("Starting call repository to add client");
            try
            {
                var newId = await _connectionProvider.QuerySingleOrDefaultAsync<int>(ClientsHelper.SP_ADD_CLIENT, parameters);
                client.ClientId = newId;

                _logger.LogInformation("Client added successfully");
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding client");
                throw;
            }
        }

        public async Task<Client> UpdateClient(Client client, int clientId)
        {
            var parameters = new
            {
                ClientId = clientId,
                client.Name,
                client.LastName,
                client.Phone,
                client.Email,
                client.StartDate
            };

            _logger.LogInformation("Starting call repository to update client");
            try
            {
                await _connectionProvider.ExecuteAsync(ClientsHelper.SP_UPDATE_CLIENT, parameters);

                _logger.LogInformation("Client updated successfully");
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating client");
                throw;
            }
        }

        public async Task<bool> DeleteClient(int clientId)
        {
            _logger.LogInformation("Starting call repository to delete client.");

            try
            {
                var rowsAffected = await _connectionProvider.ExecuteAsync(
                ClientsHelper.SP_DELETE_CLIENT,
                new { ClientId = clientId });

                _logger.LogInformation("Client deleted successfully.");
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting client.");
                throw;
            }
        }
    }
}
