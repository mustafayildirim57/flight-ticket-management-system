using Microsoft.EntityFrameworkCore;

namespace FlightProjectManagementApi.DataModels
{
    public class FlightProjectAdminContext : DbContext
    {
        public FlightProjectAdminContext(DbContextOptions<FlightProjectAdminContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
