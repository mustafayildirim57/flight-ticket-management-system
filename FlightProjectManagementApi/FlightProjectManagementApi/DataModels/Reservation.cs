using System.ComponentModel.DataAnnotations;

namespace FlightProjectManagementApi.DataModels
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int FlightId { get; set; }  // Sadece FlightId zorunlu

        public Flight? Flight { get; set; }  // Flight nesnesini controller'da ilişkilendiriyoruz

        [Required]
        public int UserId { get; set; }  // Sadece UserId zorunlu

        public User? User { get; set; }  // User nesnesini controller'da ilişkilendiriyoruz

        public int NumberOfSeats { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
