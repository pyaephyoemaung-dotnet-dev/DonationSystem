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
                
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                
                string fileName = $"{Guid.NewGuid()}_{postImage.FileName}";
                string filePath = Path.Combine(uploadsFolder, fileName);

                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postImage.CopyToAsync(stream);
                }

               
                imagePath = $"/uploads/{fileName}";
            }

            var post = new PostModel
            {
                name = user.name,
                description = model.description,
                postImage = imagePath!,  
                profile = user.profile,
                userId = user.userId,
                type = user.type,
                title = model.title,
                role = user.role
            };

            _db.PostBlog.Add(post);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "User", new { userId = user.userId });
        }


    }
}
