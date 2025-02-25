using DonationSystem.DataBase;
using DonationSystem.Models;
using Microsoft.AspNetCore.Authorization;
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

           
            var userPosts = await _db.PostBlog
                .Where(x => x.userId == userId)
                .OrderByDescending(x => x.created_at) 
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
        [HttpGet("/User/EditDetail/{userId}")]
        public async Task<IActionResult> EditDetail(string userId)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");
            return View("EditDetail", user);
        }
        [HttpPost("/User/UpdateDetail/{userId}")]
        public async Task<IActionResult> UpdateDetail(string userId,SignupModel signup, IFormFile profile)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");
            string imagePath = null;
            if (profile != null && profile.Length > 0)
            {
                // Ensure the "wwwroot/uploads" directory exists
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                // Create a unique file name
                string fileName = $"{Guid.NewGuid()}_{profile.FileName}";
                string filePath = Path.Combine(uploadsFolder, fileName);
                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profile.CopyToAsync(stream);
                }
                // Store only the relative path
                imagePath = $"/uploads/{fileName}";
            }
            user.name = signup.name;
            user.email = signup.email;
            user.profile = imagePath!;
            user.phone = signup.phone;
            user.address = signup.address;
            user.password = signup.password;
            user.type = signup.type;
            user.role = signup.role;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "User", new { userId = user.userId });
        }
        [HttpGet("/User/History/{userId}")]
        public async Task<IActionResult>History(string userId)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");

            Campain campain = new Campain
            {
                signupModel = user,
               donation = await _db.Donations.Where(x => x.userId == userId).ToListAsync()
            };
            return View("History", campain);
        }
        [HttpGet("/User/Create/{userId}")]
        public async Task<IActionResult>Create(string userId)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");
            return View(user);
        }
        [HttpPost("/User/Save/{userId}")]
        public async Task<IActionResult>Save(string userId, Donation donation)
        {
            var user = await _db.SignUp.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");
            var donate = new Donation
            {
                address = donation.address,
                phone = donation.phone,
                DonorName = donation.DonorName,
                userId = user.userId,
                Amount = donation.Amount,
                DonationDate = donation.DonationDate,
                Email = donation.Email
            };
            _db.Donations.Add(donate);
            await _db.SaveChangesAsync();
            return RedirectToAction("History", "User", new { userId = user.userId });
        }
        [HttpGet("/User/DeleteList/{userId}")]
        public async Task<IActionResult> DeleteList(string userId)
        {
            var user = await _db.Donations.FirstOrDefaultAsync(x => x.userId == userId);
            if (user == null) return Redirect("/Errors");
            _db.Donations.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("History", "User", new { userId = user.userId });
        }
    }
}
