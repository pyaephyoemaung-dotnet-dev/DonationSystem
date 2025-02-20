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
        public DbSet<PostModel> PostBlog { get; set; }
        public DbSet<CampainModel> Campains { get; set; }
        public DbSet<ListModel> Blog { get; set; }
        public DbSet<SessionModel> SessionBlog { get; set; }
        public DbSet<ProfileModel> Profile { get; set; }
    }
    public class UserProfile
    {
        public SignupModel signupModel { get; set; }
        public List<PostModel> postModels { get; set; }
    }
}
