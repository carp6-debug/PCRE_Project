using Microsoft.AspNetCore.Mvc;
using PCRE.Data;
using PCRE.Models;

namespace PCRE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CapacitorController : ControllerBase
    {
        // 1. We stick with the name '_repository' as defined in your constructor
        private readonly IPrognosticsRepository _repository;

        public CapacitorController(IPrognosticsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<TransientReading>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<TransientReading>>> GetCapacitorData(int id)
        {
            try
            {
                // FIX: Changed 'GetCapacitorHealthAsync' to 'GetCapacitorData' to match your Repository
                var results = await _repository.GetCapacitorData(id, 100);
                
                if (results == null || !results.Any())
                {
                    return NotFound(new { message = $"No telemetry data found for Capacitor ID {id}" });
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}/alerts")]
        public async Task<IActionResult> GetAlerts(int id, [FromQuery] double threshold = 4.0)
        {
            // FIX: Changed '_repo' to '_repository' to match the private field above
            var alerts = await _repository.GetReadingsBelowThreshold(id, threshold);
            return Ok(alerts);
        }

        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetStatus(int id)
        {
            var latest = await _repository.GetLatestStatus(id);
            if (latest == null) return NotFound();
            
            // Defensive coding: Return a simplified object to save bandwidth
            return Ok(new {
                latest.ReadingId,
                latest.HumanDate,
                latest.AvgVoltage,
                latest.HealthIndex,
                latest.Status
            });
        }
    }
}