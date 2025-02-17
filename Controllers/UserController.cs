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

        public IActionResult Index()
        {
            return View();
        }
    }
}
