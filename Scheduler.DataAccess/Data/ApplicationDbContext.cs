using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scheduler.Models;

namespace Scheduler.DataAccess
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
        }


        public DbSet<CodeSet> CodeSets { get; set; }
        public DbSet<CodeValue> CodeValues { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<OrderCatalog> OrderCatalogs { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ProviderScheduleProfile> ProviderScheduleProfiles { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<SchAppt> SchAppts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<ProviderScheduleProfile>()
                .HasOne(m => m.Location)
                .WithMany()
                .HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Encounter>()
                .HasOne(m => m.Location)
                .WithMany()
                .HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Encounter>()
                .HasOne(m => m.SchAppt)
                .WithMany()
                .HasForeignKey(m => m.SchApptId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<SchAppt>()
               .HasOne(m => m.ApptType)
               .WithMany()
               .HasForeignKey(m => m.ApptTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<SchAppt>()
                .HasOne(m => m.ProviderScheduleProfile)
                .WithMany()
                .HasForeignKey(m => m.ProviderScheduleProfileId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
