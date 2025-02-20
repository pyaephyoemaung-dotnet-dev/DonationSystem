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

            List<PostModel> post = new List<PostModel>
            {
                new PostModel
                {
                    type = user.type,
                    name = user.name,
                    userId = user.userId
                }
            };
           

            UserProfile userProfile = new UserProfile
            {
                signupModel = user,
                postModels = post
            };
            
            return View(userProfile);
        }

    }
}
