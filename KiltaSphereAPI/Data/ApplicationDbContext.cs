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
    }
}