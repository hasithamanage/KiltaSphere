using KiltaSphereAPI.Models; // Import the Member model 
using Microsoft.EntityFrameworkCore;

namespace KiltaSphereAPI.Data
{
    // Inherit from DbContext, which is the core EF Core class
    public class ApplicationDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions (used for configuration)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet represents a table in the database
        public DbSet<Member> Members { get; set; }

        // Add the DbSet for Payment
        public DbSet<Payment> Payments { get; set; }

        // Add CommunicationLog DB sets 
        public DbSet<CommunicationLog> CommunicationLogs { get; set; }


        // This method runs when the models are being created/configured
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // [Data seeding/testing]
            modelBuilder.Entity<Member>().HasData(
                new Member { MemberId = 1, FirstName = "Aino", LastName = "Mäkinen", Email = "aino.m@example.fi", JoiningDate = DateTime.Now.AddYears(-5), MembershipStatus = "Active" },
                new Member { MemberId = 2, FirstName = "Elias", LastName = "Virtanen", Email = "elias.v@example.fi", JoiningDate = DateTime.Now.AddYears(-1), MembershipStatus = "Pending" },
                new Member { MemberId = 3, FirstName = "Sofi", LastName = "Korhonen", Email = "sofi.k@example.fi", JoiningDate = DateTime.Now.AddMonths(-3), MembershipStatus = "Active" }
            );
        }

    }

}