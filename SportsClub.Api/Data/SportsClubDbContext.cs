using Microsoft.EntityFrameworkCore;
using SportsClub.Api.Entities;

namespace SportsClub.Api.Data
{
    public class SportsClubDbContext : DbContext
    {
        public SportsClubDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            // Suppress warning about pending model changes during migrations
            optionsBuilder.ConfigureWarnings(w =>
                w.Log(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base call is only needed when inheriting from IdentityDbContext, default implementation does nothing
            base.OnModelCreating(modelBuilder);

            // Configure Workout.Price precision
            modelBuilder.Entity<Workout>()
                .Property(w => w.Price)
                .HasPrecision(18, 2);

            // For EF Data seeding, see: https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
            modelBuilder.Entity<Location>().HasData(SportClubSeedData.Locations);
            modelBuilder.Entity<Workout>().HasData(SportClubSeedData.Workouts);
            modelBuilder.Entity<Lesson>().HasData(SportClubSeedData.Lessons);
            modelBuilder.Entity<User>().HasData(SportClubSeedData.Users);
        }

        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Workout> Workouts => Set<Workout>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    }
}
