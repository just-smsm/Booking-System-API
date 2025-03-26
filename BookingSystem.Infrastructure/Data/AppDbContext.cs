using BookingSystem.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships
            builder.Entity<Reservation>()
                .HasOne(r => r.Trip)
                .WithMany()
                .HasForeignKey(r => r.TripId);

            builder.Entity<Reservation>()
                .HasOne(r => r.ReservedBy)
                .WithMany()
                .HasForeignKey(r => r.ReservedById);
        }
    }
}