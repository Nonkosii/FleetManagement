using FleetManagement.Data;
using FleetManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehiclesApiController : ControllerBase
    {
        private readonly FleetDbContext _context;

        public VehiclesApiController(FleetDbContext context)
        {
            _context = context;
        }

       
        [HttpPost("location")]
        public async Task<IActionResult> PostLocation([FromBody] VehicleLocation location)
        {
            if (location == null || string.IsNullOrEmpty(location.VehicleId))
            {
                return BadRequest("Vehicle Id and location data are required.");
            }

            if (location.Latitude < -90 || location.Latitude > 90)
            {
                return BadRequest("Invalid latitude value.");
            }

            if (location.Longitude < -180 || location.Longitude > 180)
            {
                return BadRequest("Invalid longitude value.");
            }

            location.Timestamp = DateTime.UtcNow;
            await _context.VehicleLocations.AddAsync(location);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Location of the vehicle saved successfully" });
        }

        
        [HttpGet("locations")]
        public IActionResult GetLatestLocations()
        {
            var latestLocations = _context.VehicleLocations
                .GroupBy(v => v.VehicleId)
                .Select(g => g.OrderByDescending(v => v.Timestamp).FirstOrDefault())
                .ToList();

            return Ok(latestLocations);  // This returns JSON data
        }
    }
}
