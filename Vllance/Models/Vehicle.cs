using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vllance.Models;

public class Vehicle
{
    [Key]
    public int Id { get; set; }

    [Required]
    public VehicleType Type { get; set; }

    [Required]
    [MaxLength(100)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Color { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Identifier { get; set; } = string.Empty; // Plate number, dents, scratches, etc.

    [MaxLength(500)]
    public string? ImagePath { get; set; } // Path or URL to vehicle image

    public bool IsLocked { get; set; } = false;

    [MaxLength(100)]
    public string? Coordinate { get; set; } // x,y coordinates
    
    // Foreign key: Each Vehicle belongs to one User
    [Required]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}

public enum VehicleType
{
    Bicycle,
    Car,
    Motorcycle,
    Bus,
    Truck
}
