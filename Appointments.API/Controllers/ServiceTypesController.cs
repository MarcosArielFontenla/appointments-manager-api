﻿using Appointments.Domain.DTOs;
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
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ServiceTypeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ServiceTypeDTO>>> GetServiceTypesAsync()
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
