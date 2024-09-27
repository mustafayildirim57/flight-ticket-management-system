using System.ComponentModel.DataAnnotations.Schema;

namespace FlightProjectManagementApi.DataModels
{
    public class Flight
    {
        public int Id { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime FlightDate { get; set; }
        public TimeSpan FlightTime { get; set; }
        public int Capacity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
