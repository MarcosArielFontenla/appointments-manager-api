using Appointments.Domain.DTOs;
using Appointments.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientService _clientService;

        public ClientsController(ILogger<ClientsController> logger, IClientService clientService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        /// <summary>
        /// Get All clients
        /// </summary>
        /// <returns>A list of clients</returns>
        /// <response code="200">Returns the list of clients.</response>
        /// <response code="404">If no clients are found.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ClientResponse>>> GetClients()
        {
            _logger.LogInformation("Starting call service to getting all clients");

            try
            {
                var response = await _clientService.GetClientsAsync().ConfigureAwait(false);

                if (response is null)
                {
                   _logger.LogError("No clients found");
                    return NotFound("No clients found");
                }
                _logger.LogInformation("Clients found successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting clients");
                throw;
            }
        }

        /// <summary>
        /// Retrieve a client by id.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns>A client by id</returns>
        [HttpGet("{clientId}")]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientResponse>> GetClientById(int clientId)
        {
            _logger.LogInformation("Starting call service to getting client by id");

            try
            {
                var response = await _clientService.GetClientByIdAsync(clientId).ConfigureAwait(false);

                if (response is null)
                {
                    _logger.LogError("No client found");
                    return NotFound("No client found");
                }
                _logger.LogInformation("Client found successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting client by id");
                throw;
            }
        }
    }
}
