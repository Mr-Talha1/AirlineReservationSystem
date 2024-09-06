using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservationSystem.Models;

public partial class Airline
{
    public int AirlineId { get; set; }
    
    public string? ImagePath { get; set; }
    [Required]
    public string AirlineName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
    [NotMapped]
    public IFormFile? AilrineImagePath { get; set; }
}
