using DonationSystem.DataBase;
using DonationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;

    public HomeController(ILogger<HomeController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult LoadContent(string section)
    {
        _logger.LogInformation("Section received: {Section}", section);

        List<ListModel> items = null;

        if (section == "bobwr")
        {
            items = _db.Blog.Where(x => x.categories == "bobwr").ToList();
            return PartialView("_Bobwryeiktar", items);
        }
        else if (section == "miba")
        {
            items = _db.Blog.Where(x => x.categories == "miba").ToList();
            return PartialView("_Miba", items);
        }
        else if (section == "yaybay")
        {
            items = _db.Blog.Where(x => x.categories == "yaybay").ToList();
            return PartialView("_Yaybay", items);
        }
        return PartialView("_Mibay", _db.Blog.ToList()); 
        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}