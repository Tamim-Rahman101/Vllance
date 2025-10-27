using System.ComponentModel.DataAnnotations;

namespace Vllance.Models;

public class Admin
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

    // Navigation property: One Admin can handle multiple Zones
    public ICollection<Zone> Zones { get; set; } = new List<Zone>();
}
