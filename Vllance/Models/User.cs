namespace Vllance.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    
    // Navigation property for vehicles
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
