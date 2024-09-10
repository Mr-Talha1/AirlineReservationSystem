using System;
using System.Collections.Generic;

namespace AirlineReservationSystem.Models;

public partial class Flight
{
    public int FlightId { get; set; }

    public string FlightNumber { get; set; } = null!;

    public int? AirlineId { get; set; }

    public int? OriginCityId { get; set; }

    public int? DestinationCityId { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public int TotalSeats { get; set; }

    public int? AvailableSeats { get; set; }

    public int? ClassId { get; set; }

    public int SkyMiles { get; set; }

    public int? Stops { get; set; }

    public decimal Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Airline? Airline { get; set; }

    public virtual Class? Class { get; set; }

    public virtual City? DestinationCity { get; set; }

    public virtual City? OriginCity { get; set; }
}
