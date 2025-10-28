using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vllance.Data;
using Vllance.Filters;

namespace Vllance.Controllers;

[RoleAuthorization("Admin")]
public class AdminDashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminDashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Get admin information from session
        var adminId = HttpContext.Session.GetInt32("UserId");
        var adminEmail = HttpContext.Session.GetString("UserEmail");

        if (adminId.HasValue)
        {
            var admin = await _context.Admins.FindAsync(adminId.Value);
            ViewBag.AdminName = admin?.FullName ?? "Admin";
        }

        // Get statistics for the dashboard
        var totalZones = await _context.Zones.CountAsync();
        var totalGuards = await _context.Guards.CountAsync();
        var totalUsers = await _context.Users.CountAsync();
        var totalVehicles = await _context.Vehicles.CountAsync();

        ViewBag.TotalZones = totalZones;
        ViewBag.TotalGuards = totalGuards;
        ViewBag.TotalUsers = totalUsers;
        ViewBag.TotalVehicles = totalVehicles;

        return View();
    }
}
