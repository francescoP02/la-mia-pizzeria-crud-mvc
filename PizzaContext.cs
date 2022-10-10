using la_mia_pizzeria_static;
using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> pizzasList { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Ingredient> ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-pizza;Integrated Security=True");
        }
    }
}
