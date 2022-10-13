using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    [Authorize]
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
                //List<Pizza> pizzas = context.pizzasList.ToList<Pizza>();
                return View("Index", context.pizzasList.Include(p => p.Category).Include("Ingredients").ToList());
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            using (PizzaContext context = new PizzaContext())
            {
                List<Category> categories = context.categories.ToList();
                List<Ingredient> ingredients = context.Ingredients.ToList();
                PizzaCategory model = new PizzaCategory();
                model.Categories = categories;
                model.Ingredients = ingredients;
                model.Pizza = new Pizza();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategory pizzaData)
        {
            if (!ModelState.IsValid)
            {
                pizzaData.Categories = new PizzaContext().categories.ToList();
                pizzaData.Ingredients = new PizzaContext().Ingredients.ToList();
                return View("Create", pizzaData);
            }

            using (PizzaContext context = new PizzaContext())
            {
                //Pizza pizza = new Pizza();
                //pizza.Name = formData.Name;
                //pizza.Description = formData.Description;
                //pizza.Photo = formData.Photo;
                //pizza.Price = formData.Price;

                pizzaData.Pizza.Ingredients = context.Ingredients.Where(ingredient => pizzaData.selectedIngredients.Contains(ingredient.Id)).ToList();

                context.pizzasList.Add(pizzaData.Pizza);

                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult EditForm(int Id)
        {
            //PizzaContext context = new PizzaContext();
            using (PizzaContext context = new PizzaContext())
            {
                try
                {
                    Pizza PizzaToEdit = context.pizzasList.Where(x => x.Id == Id).Include("Ingredients").First();

                    if (PizzaToEdit == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        PizzaCategory pizzaCategory = new PizzaCategory();
                        pizzaCategory.Pizza = PizzaToEdit;
                        pizzaCategory.Categories = context.categories.ToList();
                        pizzaCategory.Ingredients = context.Ingredients.ToList();
                        return View(pizzaCategory);
                    }
                }
                catch
                {
                    return View("Error");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, PizzaCategory dataForms)
        {
            using (PizzaContext context = new PizzaContext())
            {
                if (!ModelState.IsValid)
                {
                    dataForms.Categories = context.categories.ToList();
                    dataForms.Ingredients = context.Ingredients.ToList();
                    return View("EditForm", dataForms);
                }

                Pizza pizza = context.pizzasList.Where(pizza => pizza.Id == Id).Include("Ingredients").FirstOrDefault();

                pizza.Name = dataForms.Pizza.Name;
                pizza.Description = dataForms.Pizza.Description;
                pizza.Photo = dataForms.Pizza.Photo;
                pizza.Price = dataForms.Pizza.Price;
                pizza.CategoryId = dataForms.Pizza.CategoryId;
                pizza.Ingredients = context.Ingredients.Where(ingredient => dataForms.selectedIngredients.Contains(ingredient.Id)).ToList<Ingredient>();
                

                context.SaveChanges();

                return RedirectToAction("Index");

            }
            
        }

        public IActionResult Show(int id)
        {

            using (PizzaContext db = new PizzaContext())
            {

                try
                {
                    Pizza toShow = db.pizzasList.Where(x => x.Id == id).Include(pizza => pizza.Category).Include("Ingredients").FirstOrDefault();
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