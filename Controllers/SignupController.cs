using DonationSystem.DataBase;
using DonationSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DonationSystem.Controllers
{
    public class SignupController : Controller
    {
        private readonly AppDbContext _db;

        public SignupController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [ActionName("Create")]
        [HttpPost]
        public async Task<IActionResult> Index(SignupModel signupModel)
        {
            await _db.SignUp.AddAsync(signupModel);
            await _db.SaveChangesAsync();
            return Redirect("/Home");
        }
    }
}
