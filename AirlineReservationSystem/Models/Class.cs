using System;
using System.Collections.Generic;

namespace AirlineReservationSystem.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
