using DonationSystem.DataBase;
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
        [HttpGet("{userId}")]
        public async Task<IActionResult> Index(string userId)
        {
            var user  = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);
            if(user is null) return Redirect("/Errors");

            return View(user);
        }
    }
}
