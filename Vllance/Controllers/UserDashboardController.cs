using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vllance.Data;
using Vllance.Filters;

namespace Vllance.Controllers;

[RoleAuthorization("User")]
public class UserDashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserDashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Get user information from session
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId.HasValue)
        {
            var user = await _context.Users.FindAsync(userId.Value);
            ViewBag.UserName = user?.FullName ?? "User";

            // Get user's vehicles
            var userVehicles = await _context.Vehicles
                .Where(v => v.UserId == userId.Value)
                .ToListAsync();

            ViewBag.TotalVehicles = userVehicles.Count;
            ViewBag.LockedVehicles = userVehicles.Count(v => v.IsLocked);
            ViewBag.UnlockedVehicles = userVehicles.Count - ViewBag.LockedVehicles;
        }
        else
        {
            ViewBag.TotalVehicles = 0;
            ViewBag.LockedVehicles = 0;
            ViewBag.UnlockedVehicles = 0;
        }

        return View();
    }
}
