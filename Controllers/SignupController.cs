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
        public async Task<IActionResult> Index(SignupModel signupModel, IFormFile profile)
        {
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

               
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profile.CopyToAsync(stream);
                }

                
                imagePath = $"/uploads/{fileName}";
            }
            var user = new SignupModel
            {
                name = signupModel.name,
                email = signupModel.email,
                password = signupModel.password,
                type = signupModel.type,
                role = signupModel.role,
                phone = signupModel.phone,
                profile = imagePath!,
                address = signupModel.address
            };
            await _db.SignUp.AddAsync(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "User", new { userId = user.userId });
        }
    }
}
