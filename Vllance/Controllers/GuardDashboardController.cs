using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vllance.Data;
using Vllance.Filters;

namespace Vllance.Controllers;

[RoleAuthorization("Guard")]
public class GuardDashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public GuardDashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Get guard information from session
        var guardId = HttpContext.Session.GetInt32("UserId");

        if (guardId.HasValue)
        {
            var guard = await _context.Guards
                .Include(g => g.Zone)
                .FirstOrDefaultAsync(g => g.Id == guardId.Value);

            ViewBag.GuardName = guard?.FullName ?? "Guard";
            ViewBag.GuardShift = guard?.Shift ?? "N/A";
            ViewBag.ZoneName = guard?.Zone?.Name ?? "Not Assigned";

            // For now, show all vehicles (in future, filter by zone when that relationship exists)
            var allVehicles = await _context.Vehicles.ToListAsync();

            ViewBag.TotalVehiclesInZone = allVehicles.Count;
            ViewBag.LockedVehicles = allVehicles.Count(v => v.IsLocked);
            ViewBag.UnlockedVehicles = allVehicles.Count - ViewBag.LockedVehicles;
        }
        else
        {
            ViewBag.TotalVehiclesInZone = 0;
            ViewBag.LockedVehicles = 0;
            ViewBag.UnlockedVehicles = 0;
        }

        return View();
    }
}
