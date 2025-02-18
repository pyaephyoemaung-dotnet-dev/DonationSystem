using DonationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationSystem.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<SignupModel> SignUp { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CampainModel> Campains { get; set; }
        public DbSet<ListModel> Blog { get; set; }
        public DbSet<SessionModel> SessionBlog { get; set; }
    }   
}
