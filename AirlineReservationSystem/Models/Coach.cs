using System;
using System.Collections.Generic;

namespace AirlineReservationSystem.Models;

public partial class Coach
{
    public int CoachId { get; set; }

    public string CoachName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
