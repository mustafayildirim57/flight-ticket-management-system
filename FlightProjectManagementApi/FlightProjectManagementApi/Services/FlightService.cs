using FlightProjectManagementApi.DataModels;
using Microsoft.EntityFrameworkCore;

namespace FlightProjectManagementApi.Services
{
    // FlightService.cs
    public class FlightService
    {
        private readonly FlightProjectAdminContext _context;

        public FlightService(FlightProjectAdminContext context)
        {
            _context = context;
        }

        public async Task<List<Flight>> GetFlightsAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight?> GetFlightByIdAsync(int id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task<Flight> AddFlightAsync(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<bool> UpdateFlightAsync(int id, Flight flight)
        {
            if (id != flight.Id)
            {
                return false;
            }

            _context.Entry(flight).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return false;
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Flight>> FilterFlightsAsync(string? departureCity, string? arrivalCity, DateTime? flightDate)
        {
            var query = _context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(departureCity))
            {
                query = query.Where(f => f.DepartureCity == departureCity);
            }

            if (!string.IsNullOrEmpty(arrivalCity))
            {
                query = query.Where(f => f.ArrivalCity == arrivalCity);
            }

            if (flightDate.HasValue)
            {
                query = query.Where(f => f.FlightDate.Date == flightDate.Value.Date);
            }

            return await query.ToListAsync();
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }
    }

}
