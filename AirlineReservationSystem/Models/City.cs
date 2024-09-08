using System;
using System.Collections.Generic;

namespace AirlineReservationSystem.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CountryName { get; set; } = null!;

    public string CityName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Flight> FlightDestinationCities { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightOriginCities { get; set; } = new List<Flight>();
}
