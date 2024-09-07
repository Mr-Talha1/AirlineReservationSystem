using System;
using System.Collections.Generic;

namespace AirlineReservationSystem.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CountryName { get; set; } = null!;

    public string CityName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
