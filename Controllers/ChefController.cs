using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;

namespace ChefsNDishes.Controllers;

public class ChefController : Controller
{
    private readonly ILogger<ChefController> _logger;
    private MyContext db;

    public ChefController(ILogger<ChefController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }
    [HttpGet("chefs/new")]
    public IActionResult NewChef()
    {
        return View("NewChef");
    }
    [HttpPost("chefs/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if (!ModelState.IsValid)
        {
            return View("NewChef");
        }

        db.Chefs.Add(newChef);

        db.SaveChanges();

        return RedirectToAction("Index", "Home");
    }

    // [HttpGet("chefs/{id}")]
    // public IActionResult ViewChef(int id)
    // {
    //     Chef? chef = db.Chefs.FirstOrDefault(chef => chef.ChefId == id);

    //     if(chef == null)
    //     {
    //         return RedirectToAction("Index");
    //     }
    //     return View("Details", chef);
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}