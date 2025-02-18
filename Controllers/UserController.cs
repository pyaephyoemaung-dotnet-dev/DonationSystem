using DonationSystem.DataBase;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index(string userId)
        {
            var user = _db.SignUp.FirstOrDefault(x => x.userId == userId);
            if (user is null) return Redirect("/Errors");
            return View(user);
        }
    }
}
