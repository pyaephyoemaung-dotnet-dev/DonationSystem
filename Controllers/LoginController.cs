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
            string SessionId = Guid.NewGuid().ToString();
            DateTime SessionExpired = DateTime.Now.AddMinutes(30);

            CookieOptions cookie = new CookieOptions();
            cookie.Expires = SessionExpired;
            Response.Cookies.Append("userId", item.userId, cookie);
            Response.Cookies.Append("sessionId", SessionId.ToString(), cookie);
            await _db.SessionBlog.AddAsync(new SessionModel
            {
                sessionId = SessionId,
                userId = item.userId,
                sessionExpired = SessionExpired
            });
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "User", new { userId = item.userId });
        }
    }
}
