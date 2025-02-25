using DonationSystem.DataBase;
using DonationSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LoadContent(string section)
        {
            List<PostModel> items = null;
            List<SignupModel> signups = null;

            if (section == "dashboard")
            {
                var user = _db.SignUp.OrderByDescending(x => x.Id).ToList();
                var post = _db.PostBlog.OrderByDescending(x => x.Id).ToList();
                Dashboard dashboard = new Dashboard
                {
                    signupModels = user,
                    postModels = post
                };
                return PartialView("_Dashboard", dashboard);
            }
            else if (section == "posts")
            {
                items = _db.PostBlog.ToList();
                return PartialView("_Posts", items);
            }
            else if (section == "users")
            {
                signups = _db.SignUp.ToList();
                return PartialView("_Users", signups);
            }
            var users = _db.SignUp.ToList();
            var posts = _db.PostBlog.ToList();
            Dashboard dashboards = new Dashboard
            {
                signupModels = users,
                postModels = posts
            };
            return PartialView("_Default", dashboards);
        }
        [HttpGet("/Admin/Delete/{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);
            var post = await _db.PostBlog.FirstOrDefaultAsync(x => x.userId == user.userId);
            if (user == null) return Redirect("/Errors");
            if (post == null) return Redirect("/Errors");
            _db.SignUp.Remove(user);
            _db.PostBlog.RemoveRange(post);
            await _db.SaveChangesAsync();

            var updatedUsers = _db.SignUp.OrderByDescending(x => x.Id).ToList();
            return View("Index",updatedUsers); 
        }
    }
}
