using FleetManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FleetManagement.Data
{
   
    public class FleetDbContext : DbContext
    {
        public FleetDbContext(DbContextOptions<FleetDbContext> options) : base(options) { }

       
        public DbSet<VehicleLocation> VehicleLocations { get; set; }

       
    }
}
