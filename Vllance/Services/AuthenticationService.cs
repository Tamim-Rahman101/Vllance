using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Vllance.Data;
using Vllance.Models;

namespace Vllance.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationDbContext _context;

    public AuthenticationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string? Role, int? UserId, string? ErrorMessage)> AuthenticateAsync(
        string email, string password, string role)
    {
        try
        {
            switch (role.ToLower())
            {
                case "admin":
                    var admin = await _context.Admins
                        .FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());
                    
                    if (admin == null)
                        return (false, null, null, "Invalid email or password.");
                    
                    if (!VerifyPassword(password, admin.PasswordHash))
                        return (false, null, null, "Invalid email or password.");
                    
                    // Update last login
                    admin.LastLoginAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    
                    return (true, "Admin", admin.Id, null);

                case "user":
                    var user = await _context.Users
                        .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
                    
                    if (user == null)
                        return (false, null, null, "Invalid email or password.");
                    
                    if (!VerifyPassword(password, user.PasswordHash))
                        return (false, null, null, "Invalid email or password.");
                    
                    // Update last login
                    user.LastLoginAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    
                    return (true, "User", user.Id, null);

                case "guard":
                    var guard = await _context.Guards
                        .FirstOrDefaultAsync(g => g.Email.ToLower() == email.ToLower());
                    
                    if (guard == null)
                        return (false, null, null, "Invalid email or password.");
                    
                    if (!VerifyPassword(password, guard.PasswordHash))
                        return (false, null, null, "Invalid email or password.");
                    
                    // Update last login
                    guard.LastLoginAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    
                    return (true, "Guard", guard.Id, null);

                default:
                    return (false, null, null, "Invalid role selected.");
            }
        }
        catch (Exception ex)
        {
            return (false, null, null, $"An error occurred during authentication: {ex.Message}");
        }
    }

    public async Task<(bool Success, string? ErrorMessage)> RegisterAsync(
        string role, string fullName, string email, string phoneNumber,
        string institute, string password, string? shift = null)
    {
        try
        {
            // Check if email already exists in any role
            var emailExists = await CheckEmailExistsAsync(email);
            if (emailExists)
                return (false, "An account with this email already exists.");

            var passwordHash = HashPassword(password);

            switch (role.ToLower())
            {
                case "admin":
                    var admin = new Admin
                    {
                        FullName = fullName,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        Institute = institute,
                        PasswordHash = passwordHash,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.Admins.Add(admin);
                    break;

                case "user":
                    var user = new User
                    {
                        FullName = fullName,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        Institute = institute,
                        PasswordHash = passwordHash,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.Users.Add(user);
                    break;

                case "guard":
                    if (string.IsNullOrEmpty(shift))
                        return (false, "Shift is required for Guard role.");

                    var guard = new Guard
                    {
                        FullName = fullName,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        Institute = institute,
                        Shift = shift,
                        PasswordHash = passwordHash,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.Guards.Add(guard);
                    break;

                default:
                    return (false, "Invalid role selected.");
            }

            await _context.SaveChangesAsync();
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, $"An error occurred during registration: {ex.Message}");
        }
    }

    private async Task<bool> CheckEmailExistsAsync(string email)
    {
        var emailLower = email.ToLower();
        
        var adminExists = await _context.Admins.AnyAsync(a => a.Email.ToLower() == emailLower);
        if (adminExists) return true;

        var userExists = await _context.Users.AnyAsync(u => u.Email.ToLower() == emailLower);
        if (userExists) return true;

        var guardExists = await _context.Guards.AnyAsync(g => g.Email.ToLower() == emailLower);
        return guardExists;
    }

    public string HashPassword(string password)
    {
        // Using SHA256 for password hashing (in production, use BCrypt or Argon2)
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput == passwordHash;
    }
}
