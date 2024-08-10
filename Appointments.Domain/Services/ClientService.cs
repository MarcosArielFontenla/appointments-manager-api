using Appointments.Data.Entities;
using Appointments.Data.Repositories.Interfaces;
using Appointments.Domain.DTOs;
using Appointments.Domain.Mappers;
using Appointments.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Appointments.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly IClientRepository _clientRepository;

        public ClientService(ILogger<ClientService> logger, IClientRepository clientRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public async Task<IEnumerable<ClientResponse>> GetClientsAsync()
        {
            _logger.LogInformation("Starting call repository to getting all clients");
            var response = await _clientRepository.GetClients().ConfigureAwait(false);

            if (response is null)
            {
               _logger.LogError("No clients found");
                return Enumerable.Empty<ClientResponse>();
            }
            var mappedResponse = response.Select(Mapper.MapClientToClientResponse);
            _logger.LogInformation("Clients found successfully");
            return mappedResponse;
        }

        public async Task<ClientResponse> GetClientByIdAsync(int clientId)
        {
            _logger.LogInformation("Starting call repository to getting client by id");
            var response = await _clientRepository.GetClientById(clientId).ConfigureAwait(false);

            if (response is null)
            {
                _logger.LogError("No client found");
                return null;
            }
            var mappedResponse = Mapper.MapClientToClientResponse(response);
            _logger.LogInformation("Client found successfully");
            return mappedResponse;
        }

        public async Task<ClientResponse> AddClientAsync(Client client)
        {
            _logger.LogInformation("Starting call repository to add client");
            var mappedToClient = _clientRepository.AddClient(client);

            if (mappedToClient is null)
            {
                _logger.LogError("Client not added");
                return null;
            }
            var mappedToResponse = Mapper.MapClientToClientResponse(await mappedToClient);
            _logger.LogInformation("Client added successfully");
            return mappedToResponse;
        }

        public async Task<ClientResponse> UpdateClientAsync(Client client, int clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteClientAsync(int clientId)
        {
            throw new NotImplementedException();
        }
    }
}
