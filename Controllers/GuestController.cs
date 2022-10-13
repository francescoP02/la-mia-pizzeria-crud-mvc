using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class GuestController : Controller
    {
        private PizzaContext _db;

        public GuestController()
        {
            _db = new PizzaContext();
        }

        [HttpGet]
        public IActionResult Index()
        {
           
            return View("Index", _db.pizzasList.Include(p => p.Category).Include("Ingredients").ToList());
           
        }
        public IActionResult Show(int id)
        {
            try
            {
                Pizza toShow = _db.pizzasList.Where(x => x.Id == id).Include(pizza => pizza.Category).Include("Ingredients").FirstOrDefault();
                return View("Show", toShow);
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
