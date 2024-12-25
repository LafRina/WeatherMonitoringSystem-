using System;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;

namespace Catalog.DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Weather> Weather { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users Table Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.PasswordHash)
                      .IsRequired();
            });

            // Locations Table Configuration
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(l => l.Latitude)
                      .IsRequired();
                entity.Property(l => l.Longitude)
                      .IsRequired();
            });

            // Weather Table Configuration
            modelBuilder.Entity<Weather>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Temperature)
                      .IsRequired();
                entity.Property(w => w.Humidity)
                      .IsRequired();
                entity.Property(w => w.Pressure)
                      .IsRequired();

                // Relationship with Location
                entity.HasOne(w => w.Location)
                      .WithMany()
                      .HasForeignKey(w => w.LocationId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relationship with User
                entity.HasOne(w => w.User)
                      .WithMany()
                      .HasForeignKey(w => w.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
