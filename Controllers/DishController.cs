using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;
    private MyContext db;

    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

[HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        ViewBag.chefs=db.Chefs.ToList();
        return View("New");
    }

[HttpPost("dishes/create")]
public IActionResult CreateDish(Dish newDish)
{
    if(!ModelState.IsValid)
    {
        return View("New");
    }

    db.Dishes.Add(newDish);

    db.SaveChanges();

    return RedirectToAction("Index", "Home");
}

[HttpGet("dishes/{id}")]
public IActionResult ViewDish(int id)
{
    Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == id);

    if(dish == null)
    {
        return RedirectToAction("Index");
    }
    return View("Details", dish);
}

    [HttpGet("dishes")]
    public IActionResult ViewDishes()
    {
        List<Dish> allDishes = db.Dishes.Include(dish=> dish.Author).ToList();

        if(allDishes == null)
        {
            return RedirectToAction("Index");
        }
        return View("Dishes", allDishes);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}