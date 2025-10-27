using System.ComponentModel.DataAnnotations;

namespace Vllance.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Institute { get; set; } = string.Empty;
    
    // Navigation property: One User can have multiple Vehicles
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
