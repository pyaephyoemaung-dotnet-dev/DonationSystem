using Microsoft.AspNetCore.Mvc;

namespace DonationSystem.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
