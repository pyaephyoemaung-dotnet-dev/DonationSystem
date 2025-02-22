using DonationSystem.DataBase;
using DonationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonationSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _db;
        public UserController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet("/User/Index/{userId}")]
        public async Task<IActionResult> Index(string userId)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);

            if (user == null)
            {
                return Redirect("/Errors");
            }

            // Fetch all posts related to this user
            var userPosts = await _db.PostBlog
                .Where(p => p.userId == userId)
                .OrderByDescending(p => p.created_at) // Latest posts first
                .ToListAsync();

            UserProfile userProfile = new UserProfile
            {
                signupModel = user,
                postModels = userPosts
            };

            return View(userProfile);
        }


    }
}
