using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace FlightManagementAPI.Data
{
    public class FlightManagementDbContext : DbContext
    {
        public FlightManagementDbContext(DbContextOptions<FlightManagementDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
    }
}
