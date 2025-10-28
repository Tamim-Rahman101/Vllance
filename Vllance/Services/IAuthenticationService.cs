namespace Vllance.Services;

public interface IAuthenticationService
{
    Task<(bool Success, string? Role, int? UserId, string? ErrorMessage)> AuthenticateAsync(
        string email, string password, string role);
    
    Task<(bool Success, string? ErrorMessage)> RegisterAsync(
        string role, string fullName, string email, string phoneNumber, 
        string institute, string password, string? shift = null);
    
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}
