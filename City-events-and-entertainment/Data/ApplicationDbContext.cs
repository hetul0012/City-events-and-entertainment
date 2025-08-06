using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using City_events_and_entertainment.Models;

namespace City_events_and_entertainment.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Museum> Museums { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<MuseumFacility> MuseumFacilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Composite key for the many-to-many join table
            builder.Entity<MuseumFacility>()
                .HasKey(mf => new { mf.MuseumId, mf.FacilityId });

            builder.Entity<MuseumFacility>()
                .HasOne(mf => mf.Museum)
                .WithMany(m => m.MuseumFacilities)
                .HasForeignKey(mf => mf.MuseumId);

            builder.Entity<MuseumFacility>()
                .HasOne(mf => mf.Facility)
                .WithMany(f => f.MuseumFacilities)
                .HasForeignKey(mf => mf.FacilityId);

            // Optional: Cascade delete setup (if needed)
            builder.Entity<Booking>()
                .HasOne(b => b.Museum)
                .WithMany(m => m.Bookings)
                .HasForeignKey(b => b.MuseumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Feedback>()
                .HasOne(f => f.Museum)
                .WithMany(m => m.Feedbacks)
                .HasForeignKey(f => f.MuseumId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Museum>()
                .HasOne(m => m.Team)
                .WithMany()
                .HasForeignKey(m => m.TeamId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
