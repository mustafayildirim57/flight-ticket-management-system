using FlightProjectManagementApi.DataModels;
using FlightProjectManagementApi.DTOs;
using FlightProjectManagementApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightProjectManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            var reservations = await _reservationService.GetReservationsAsync();
            return Ok(reservations);
        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        // POST: api/Reservation/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
        {
            var newReservation = await _reservationService.CreateReservationAsync(reservationDto);
            return Ok(newReservation);
        }

        // PUT: api/Reservation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, [FromBody] ReservationDto reservationDto)
        {
            var success = await _reservationService.UpdateReservationAsync(id, reservationDto);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var success = await _reservationService.DeleteReservationAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
