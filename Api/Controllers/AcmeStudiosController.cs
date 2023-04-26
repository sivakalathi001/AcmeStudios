using AcmeStudiosApi.Models;
using AcmeStudiosApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AcmeStudiosApi.Controllers
{
    [Route("peoplespartnership/api/acmestudios")]
    [ApiController]
    public class AcmeStudiosController : ControllerBase
    {
        private readonly ILogger<AcmeStudiosController> _logger;
        private readonly IInterfaceWithDatabase _iwd;
        public AcmeStudiosController(ILogger<AcmeStudiosController> logger, IInterfaceWithDatabase iwd)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _iwd = iwd ??
                throw new ArgumentNullException(nameof(iwd));
        }

        [HttpGet("GetAllStudioItems")]
        public async Task<IActionResult> GetStudioItems()
        {
            var response = await _iwd.GetAllStudioItemsAsync();
            if (response.Success)
                return Ok(response);

            _logger.LogInformation(response.Message);
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudioItemById(int id)
        {
            var response = await _iwd.GetStudioItemByIdAsync(id);
            if (response.Success)
                return Ok(response);

            _logger.LogInformation(response.Message);
            return NotFound(response);
        }

        [HttpPost("StudioItems")]
        public async Task<IActionResult> AddStudioItem(AddStudioItemDto studioItem)
        {
            var response = await _iwd.AddStudioItemAsync(studioItem);
            if (response.Success)
                return Ok(response);

            _logger.LogInformation(response.Message);
            return NotFound(response);
        }

        [HttpPut("StudioItems")]
        public async Task<IActionResult> UpdateStudioItem(UpdateStudioItemDto studioItem)
        {
            var response = await _iwd.UpdateStudioItemAsync(studioItem);
            if (response.Success)
                return Ok(response);

            _logger.LogInformation(response.Message);
            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudioItemById(int id)
        {
            var response = await _iwd.DeleteStudioItemAsync(id);
            if (response.Success)
                return Ok(response);

            _logger.LogInformation(response.Message);
            return NotFound(response);
        }

        [HttpGet("GetAllStudioTypes")]
        public async Task<IActionResult> GetStudioItemTypes()
        {
            var response = await _iwd.GetAllStudioItemTypesAsync();
            if (response.Success)
                return Ok(response);

            _logger.LogInformation(response.Message);
            return NotFound(response);
        }
    }
}