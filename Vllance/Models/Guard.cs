using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vllance.Models;

public class Guard
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

    [Required]
    [MaxLength(50)]
    public string Shift { get; set; } = string.Empty;

    // Foreign key: Each Guard belongs to one Zone
    [Required]
    public int ZoneId { get; set; }

    [ForeignKey(nameof(ZoneId))]
    public Zone? Zone { get; set; }
}
