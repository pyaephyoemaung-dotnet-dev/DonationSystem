using DonationSystem.DataBase;
using DonationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonationSystem.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _db;

        public PostController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet("/Post/Index/{userId}")]
        public async Task<IActionResult> Index(string userId)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");
            return View(user);
        }
        [HttpPost("/Post/Index/{userId}")]
        public async Task<IActionResult> Index(string userId, PostModel model, IFormFile postImage)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);
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

            var post = new PostModel
            {
                name = user.name,
                description = model.description,
                postImage = imagePath!,  // Store the image path in the database
                profile = user.profile,
                userId = user.userId,
                type = user.type,
                title = model.title
            };

            _db.PostBlog.Add(post);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "User", new { userId = user.userId });
        }


    }
}
