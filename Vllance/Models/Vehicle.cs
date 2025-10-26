namespace Vllance.Models;

public class Vehicle
{
    public int Id { get; set; }
    public VehicleType Type { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    
    // Foreign key to User
    public int UserId { get; set; }
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
