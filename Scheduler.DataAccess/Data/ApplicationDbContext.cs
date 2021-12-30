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
        public DbSet<FinancialNumAlias> FinancialNumAliases { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Order> Orders { get; set; }

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

            modelBuilder
               .Entity<Document>()
               .HasOne(m => m.DocType)
               .WithMany()
               .HasForeignKey(m => m.DocTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<Order>()
               .HasOne(m => m.AdminRoute)
               .WithMany()
               .HasForeignKey(m => m.AdminRouteId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<Order>()
               .HasOne(m => m.AdminTime)
               .WithMany()
               .HasForeignKey(m => m.AdminTimeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<Order>()
               .HasOne(m => m.OrderStatus)
               .WithMany()
               .HasForeignKey(m => m.OrderStatusId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<Order>()
               .HasOne(m => m.OrderCatalog)
               .WithMany()
               .HasForeignKey(m => m.OrderCatalogId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
               .Entity<Order>()
               .HasOne(m => m.Patient)
               .WithMany()
               .HasForeignKey(m => m.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
