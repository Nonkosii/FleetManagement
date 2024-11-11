using FleetManagement.Data;
using FleetManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Controllers
{
    [Route("vehicles")]
    public class VehiclesController : Controller
    {
        // VIEW
        [HttpGet("locations")]
        public IActionResult LatestLocations()
        {

            return View();  
        }
    }

}
