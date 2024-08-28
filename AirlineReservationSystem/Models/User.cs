using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservationSystem.Models;

public partial class User
{
    public int UserId { get; set; }
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    [Required]
    public string PhoneNumber { get; set; } = null!;

    public string? ImagePath { get; set; }

    public string? PreferredCreditCard { get; set; }

    public string? CreditCardInfo { get; set; }

    public int? SkyMiles { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [NotMapped]
    public IFormFile? UserImagePath { get; set; }
}
