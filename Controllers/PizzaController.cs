using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {

        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {

            using (PizzaContext context = new PizzaContext())
            {
                List<Pizza> pizzas = context.pizzasList.ToList<Pizza>();
                return View("Index", pizzas);
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza formData)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", formData);
            }

            using (PizzaContext context = new PizzaContext())
            {
                Pizza pizza = new Pizza();
                pizza.Name = formData.Name;
                pizza.Description = formData.Description;
                pizza.Photo = formData.Photo;
                pizza.Price = formData.Price;

                context.pizzasList.Add(pizza);

                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult EditForm(int Id)
        {
            PizzaContext context = new PizzaContext();
            try
            {
                Pizza PizzaToEdit = context.pizzasList.Where(x => x.Id == Id).First();

                if (PizzaToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(PizzaToEdit);
                }
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, Pizza modifyPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("EditForm", modifyPizza);
            }
            PizzaContext context = new PizzaContext();
            Pizza toEdit = context.pizzasList.Where(x => x.Id == Id).First();
            if (toEdit != null)
            {
                toEdit.Name = modifyPizza.Name;
                toEdit.Description = modifyPizza.Description;
                toEdit.Price = modifyPizza.Price;
                toEdit.Photo = modifyPizza.Photo;
                context.SaveChanges();
            }
            else
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Show(int id)
        {

            using (PizzaContext db = new PizzaContext())
            {

                try
                {
                    Pizza toShow = db.pizzasList.Where(x => x.Id == id).First<Pizza>();
                    return View("Show", toShow);
                }
                catch
                {
                    return View("Error");
                }

            }

        }

        public IActionResult Delete(int Id)
        {
            PizzaContext context = new PizzaContext();
            Pizza toDelete = context.pizzasList.Where(x => x.Id == Id).First();

            if (toDelete != null)
            {
                context.pizzasList.Remove(toDelete);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }

}