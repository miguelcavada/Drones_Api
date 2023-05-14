using Microsoft.EntityFrameworkCore;

namespace Drones_Api.Data
{
    public class DronesDB : DbContext
    {
        public DronesDB(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Drone> Drones { get; set; }
        public DbSet<Medication> Medications { get; set; }
    }
}
