using Microsoft.EntityFrameworkCore;
using SkolprojektLab3.Models;

namespace SkolprojektLab3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<UserInterestRT> UserInterestsRelationshipTable { get; set; }
        public DbSet<LinkRT> LinksRelationshipTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
