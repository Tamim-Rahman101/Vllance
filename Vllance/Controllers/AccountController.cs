using Microsoft.AspNetCore.Mvc;
using Vllance.Services;

namespace Vllance.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticationService _authService;

    public AccountController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    // GET: /Account/Login
    public IActionResult Login()
    {
        // If already logged in, redirect to appropriate dashboard
        var userRole = HttpContext.Session.GetString("UserRole");
        if (!string.IsNullOrEmpty(userRole))
        {
            return RedirectToDashboard(userRole);
        }

        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string role, string email, string password)
    {
        if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ViewBag.ErrorMessage = "Please fill in all fields.";
            return View();
        }

        var result = await _authService.AuthenticateAsync(email, password, role);

        if (!result.Success)
        {
            ViewBag.ErrorMessage = result.ErrorMessage;
            return View();
        }

        // Set session variables
        HttpContext.Session.SetString("UserRole", result.Role!);
        HttpContext.Session.SetInt32("UserId", result.UserId!.Value);
        HttpContext.Session.SetString("UserEmail", email);

        // Redirect to appropriate dashboard
        return RedirectToDashboard(result.Role!);
    }

    // GET: /Account/Register
    public IActionResult Register()
    {
        // If already logged in, redirect to appropriate dashboard
        var userRole = HttpContext.Session.GetString("UserRole");
        if (!string.IsNullOrEmpty(userRole))
        {
            return RedirectToDashboard(userRole);
        }

        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string role, string fullName, string email, string phoneNumber, 
                                  string institute, string password, string confirmPassword, string? shift)
    {
        if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(fullName) || 
            string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber) ||
            string.IsNullOrEmpty(institute) || string.IsNullOrEmpty(password))
        {
            ViewBag.ErrorMessage = "Please fill in all required fields.";
            return View();
        }

        if (password != confirmPassword)
        {
            ViewBag.ErrorMessage = "Passwords do not match.";
            return View();
        }

        if (role.Equals("Guard", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(shift))
        {
            ViewBag.ErrorMessage = "Shift is required for Guard role.";
            return View();
        }

        var result = await _authService.RegisterAsync(role, fullName, email, phoneNumber, institute, password, shift);

        if (!result.Success)
        {
            ViewBag.ErrorMessage = result.ErrorMessage;
            return View();
        }

        ViewBag.SuccessMessage = "Registration successful! Please login.";
        return RedirectToAction("Login");
    }

    // GET: /Account/ForgotPassword
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // GET: /Account/AccessDenied
    public IActionResult AccessDenied()
    {
        return View();
    }

    // POST: /Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        // Clear all session data
        HttpContext.Session.Clear();
        
        // Optionally, you can also abandon the session
        // This will generate a new session ID on next request
        // HttpContext.Session.Remove("UserRole");
        // HttpContext.Session.Remove("UserId");
        // HttpContext.Session.Remove("UserEmail");
        
        // Redirect to homepage
        return RedirectToAction("Index", "Home");
    }

    // GET: /Account/Logout (for convenience)
    public IActionResult LogoutGet()
    {
        // Clear all session data
        HttpContext.Session.Clear();
        
        // Redirect to homepage
        return RedirectToAction("Index", "Home");
    }

    private IActionResult RedirectToDashboard(string role)
    {
        return role switch
        {
            "Admin" => RedirectToAction("Index", "AdminDashboard"),
            "User" => RedirectToAction("Index", "UserDashboard"),
            "Guard" => RedirectToAction("Index", "GuardDashboard"),
            _ => RedirectToAction("Index", "Home")
        };
    }
}
