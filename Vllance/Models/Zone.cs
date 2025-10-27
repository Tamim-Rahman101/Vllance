using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vllance.Models;

public class Zone
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Link { get; set; } = string.Empty; // RTSP or Camera Feed Link

    // Foreign key: Each Zone belongs to one Admin
    [Required]
    public int AdminId { get; set; }

    [ForeignKey(nameof(AdminId))]
    public Admin? Admin { get; set; }

    // Navigation property: One Zone can have multiple Guards
    public ICollection<Guard> Guards { get; set; } = new List<Guard>();
}
