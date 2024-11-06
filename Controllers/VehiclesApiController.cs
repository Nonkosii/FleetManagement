﻿using FleetManagement.Data;
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

        // POST /api/vehicles/location - For saving vehicle location data
        [HttpPost("location")]
        public async Task<IActionResult> PostLocation([FromBody] VehicleLocation location)
        {
            if (location == null || string.IsNullOrEmpty(location.VehicleId))
            {
                return BadRequest("VehicleId and location data are required.");
            }

            location.Timestamp = DateTime.UtcNow;
            await _context.VehicleLocations.AddAsync(location);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Location data saved successfully" });
        }

        // GET /api/vehicles/locations - For returning vehicle locations in JSON format (API endpoint)
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
