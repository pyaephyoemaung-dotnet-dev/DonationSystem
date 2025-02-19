using DonationSystem.DataBase;
using DonationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonationSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _db;

        public LoginController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(SignupModel signupModel)
        {
            var item = await _db.SignUp.FirstOrDefaultAsync(x => x.name == signupModel.name && x.password == signupModel.password);
            if (item is null) return View();
            return RedirectToAction("Index", "User", new { userId = item.userId });
        }
    }
}
