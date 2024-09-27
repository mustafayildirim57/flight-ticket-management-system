using FlightProjectManagementApi.DataModels;
using FlightProjectManagementApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlightProjectManagementApi.Services
{
    // ReservationService.cs
    public class ReservationService
    {
        private readonly FlightProjectAdminContext _context;

        public ReservationService(FlightProjectAdminContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Flight)
                .ToListAsync();
        }

        public async Task<Reservation?> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Flight)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reservation> CreateReservationAsync(ReservationDto reservationDto)
        {
            var reservation = new Reservation
            {
                FlightId = reservationDto.FlightId,
                UserId = reservationDto.UserId,
                NumberOfSeats = reservationDto.NumberOfSeats,
                ReservationDate = reservationDto.ReservationDate
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<bool> UpdateReservationAsync(int id, ReservationDto reservationDto)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return false;
            }

            reservation.FlightId = reservationDto.FlightId;
            reservation.UserId = reservationDto.UserId;
            reservation.NumberOfSeats = reservationDto.NumberOfSeats;
            reservation.ReservationDate = reservationDto.ReservationDate;

            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return false;
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
