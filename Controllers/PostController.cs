using DonationSystem.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonationSystem.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _db;

        public PostController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet("/Post/Index/{userId}")]
        public async Task<IActionResult> Index(string userId)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");
            return View(user);
        }
    }
}
