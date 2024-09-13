namespace AirlineReservationSystem.Models
{
    public class CompositeViewModel
    {
        public User User { get; set; }
        public IEnumerable<Flight> Flights { get; set; }
    }
}
