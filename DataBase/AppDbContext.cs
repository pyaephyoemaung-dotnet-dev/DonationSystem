using DonationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationSystem.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Webprofile> WebProfiles { get; set; }
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
    public class MessageModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class Dashboard
    {
        public List<SignupModel> signupModels { get; set; }
        public List<PostModel> postModels { get; set; }
    }
    public class Campain
    {
        public SignupModel signupModel { get; set; }
        public List<Donation> donation { get; set; }
    }
    public class Campains
    {
        public List<SignupModel> signupModel { get; set; }
        public List<Donation> donation { get; set; }
    }
}

