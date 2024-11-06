using FleetManagement.Data;
using FleetManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Controllers
{
    [Route("vehicles")]
    public class VehiclesController : Controller
    {
        private readonly FleetDbContext _context;

        public VehiclesController(FleetDbContext context)
        {
            _context = context;
        }

        // GET /vehicles/locations - For rendering a view with vehicle locations (View)
        [HttpGet("locations")]
        public IActionResult LatestLocationsView()
        {
            var latestLocations = _context.VehicleLocations
                .GroupBy(v => v.VehicleId)
                .Select(g => g.OrderByDescending(v => v.Timestamp).FirstOrDefault())
                .ToList();

            return View("LatestLocations", latestLocations);  // This renders a view
        }
    }

}
