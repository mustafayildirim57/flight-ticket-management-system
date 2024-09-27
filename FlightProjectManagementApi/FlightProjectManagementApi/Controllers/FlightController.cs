using FlightProjectManagementApi.DataModels;
using FlightProjectManagementApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightProjectManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        // GET: api/Flight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            var flights = await _flightService.GetFlightsAsync();
            return Ok(flights);
        }

        // GET: api/Flight/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        // POST: api/Flight/add
        [HttpPost("add")]
        public async Task<ActionResult<Flight>> AddFlight(Flight flight)
        {
            var newFlight = await _flightService.AddFlightAsync(flight);
            return CreatedAtAction(nameof(GetFlight), new { id = newFlight.Id }, newFlight);
        }

        // PUT: api/Flight/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            var success = await _flightService.UpdateFlightAsync(id, flight);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Flight/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var success = await _flightService.DeleteFlightAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // FILTER: api/Flight/filter
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Flight>>> FilterFlights([FromQuery] string? departureCity, [FromQuery] string? arrivalCity, [FromQuery] DateTime? flightDate)
        {
            var flights = await _flightService.FilterFlightsAsync(departureCity, arrivalCity, flightDate);
            return Ok(flights);
        }
    }
}
