using Diploma.Models;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    internal class HomeViewModel
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
