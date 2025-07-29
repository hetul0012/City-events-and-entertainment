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
        }
    }
}
