using DonationSystem.DataBase;
using DonationSystem.Models;
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
        [HttpGet("/User/Index/{userId}")]
        public async Task<IActionResult> Index(string userId)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);

            if (user == null)
            {
                return Redirect("/Errors");
            }

            // Fetch all posts related to this user
            var userPosts = await _db.PostBlog
                .Where(x => x.userId == userId)
                .OrderByDescending(x => x.created_at) // Latest posts first
                .ToListAsync();

            UserProfile userProfile = new UserProfile
            {
                signupModel = user,
                postModels = userPosts
            };

            return View(userProfile);
        }

        [HttpGet("/User/Edit/{userId}")]
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _db.PostBlog.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");
            return View("Edit", user);
        }

        [HttpPost("/User/Update/{userId}")]
        public async Task<IActionResult> Update(string userId, PostModel model, IFormFile postImage)
        {
            var user = await _db.PostBlog.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");
            string imagePath = null;
            if (postImage != null && postImage.Length > 0)
            {
                // Ensure the "wwwroot/uploads" directory exists
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                // Create a unique file name
                string fileName = $"{Guid.NewGuid()}_{postImage.FileName}";
                string filePath = Path.Combine(uploadsFolder, fileName);
                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postImage.CopyToAsync(stream);
                }
                // Store only the relative path
                imagePath = $"/uploads/{fileName}";
            }
            user.title = model.title;
            user.description = model.description;
            user.postImage = imagePath!;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "User", new { userId = user.userId });
        }

        [HttpGet("/User/Delete/{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _db.PostBlog.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");
            _db.PostBlog.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "User", new { userId = user.userId });
        }
    }
}
