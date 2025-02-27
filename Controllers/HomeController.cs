﻿using DonationSystem.DataBase;
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

        List<PostModel> items = null;

        if (section == "ဘိုးဘွားရိပ်သာ")
        {
            items = _db.PostBlog.Where(x => x.type == "ဘိုးဘွားရိပ်သာ").ToList();
            return PartialView("_Bobwryeiktar", items);
        }
        else if (section == "မိဘမဲ့")
        {
            items = _db.PostBlog.Where(x => x.type == "မိဘမဲ့").ToList();
            return PartialView("_Miba", items);
        }
        else if (section == "ရေဘေး")
        {
            items = _db.PostBlog.Where(x => x.type == "ရေဘေး").ToList();
            return PartialView("_Yaybay", items);
        }
        return PartialView("_Mibay", _db.PostBlog.ToList()); 
        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}