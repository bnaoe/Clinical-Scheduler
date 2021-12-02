using Microsoft.EntityFrameworkCore;
using Scheduler.Models;

namespace Scheduler.DataAccess
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
        }

        public DbSet<CodeSet> CodeSets { get; set; }
        public DbSet<CodeValue> CodeValues { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<OrderCatalog> OrderCatalogs { get; set; }

    }
}
