using Microsoft.EntityFrameworkCore;

namespace Diploma.Models
{
    public class DiplomaContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<User> Users { get; set; }

        public DiplomaContext(DbContextOptions<DiplomaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Test admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    ID = 1,
                    Username = "admin",
                    Password = "admin",
                    Role = "Administrator"
                });
        }
    }
}
