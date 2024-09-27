namespace FlightProjectManagementApi.DTOs
{
    public class ReservationDto
    {
        public int FlightId { get; set; }
        public int UserId { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
