﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<ProviderScheduleProfile>()
                .HasOne(l => l.Location)
                .WithMany()
                .HasForeignKey(l => l.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
