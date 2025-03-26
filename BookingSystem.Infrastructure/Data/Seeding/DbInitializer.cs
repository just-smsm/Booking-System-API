using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context, UserManager<AppUser> userManager)
        {
            await context.Database.MigrateAsync(); // Apply migrations if not applied

            //  Seed Users
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser { UserName = "admin", Email = "admin@booking.com" },
                    new AppUser { UserName = "user1", Email = "user1@example.com" },
                    new AppUser { UserName = "user2", Email = "user2@example.com" }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "User@123"); // Set default password
                }
            }

            //  Seed Trips
            if (!context.Trips.Any())
            {
                var trips = new List<Trip>
                {
                    new Trip
                    {
                        Name = "Paris Adventure",
                        CityName = "Paris",
                        Price = 1200.99m,
                        ImageUrl = "https://example.com/paris.jpg",
                        Content = "<p>Explore the beautiful city of Paris!</p>",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Trip
                    {
                        Name = "New York Tour",
                        CityName = "New York",
                        Price = 899.50m,
                        ImageUrl = "https://example.com/ny.jpg",
                        Content = "<p>Discover the Big Apple with this amazing trip.</p>",
                        CreatedAt = DateTime.UtcNow
                    },
                    new Trip
                    {
                        Name = "Tokyo Getaway",
                        CityName = "Tokyo",
                        Price = 1500.75m,
                        ImageUrl = "https://example.com/tokyo.jpg",
                        Content = "<p>Experience the wonders of Japan.</p>",
                        CreatedAt = DateTime.UtcNow
                    }
                };

                await context.Trips.AddRangeAsync(trips);
                await context.SaveChangesAsync();
            }
        }
    }
}
