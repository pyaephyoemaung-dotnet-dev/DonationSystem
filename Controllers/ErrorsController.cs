using Microsoft.AspNetCore.Mvc;

namespace DonationSystem.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
