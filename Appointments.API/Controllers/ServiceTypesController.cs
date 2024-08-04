using Appointments.Domain.DTOs;
using Appointments.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        private readonly ILogger<ServiceTypesController> _logger;
        private readonly IServiceTypesService _serviceTypesService;

        public ServiceTypesController(ILogger<ServiceTypesController> logger, IServiceTypesService serviceTypesService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceTypesService = serviceTypesService ?? throw new ArgumentNullException(nameof(serviceTypesService));
        }

        /// <summary>
        /// Get All service types
        /// </summary>
        /// <returns>A list of service types</returns>
        /// <response code="200">Returns the list of service types.</response>
        /// <response code="404">If no service types are found.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ServiceTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ServiceTypeResponse>>> GetServiceTypes()
        {
            try
            {
                _logger.LogInformation("Starting call service to getting all service types");
                var response = await _serviceTypesService.GetServiceTypesAsync().ConfigureAwait(false);

                if (response is null)
                {
                    _logger.LogError("No service types found");
                    return NotFound("No service types found");
                }

                _logger.LogInformation("Service types found successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting service types");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
