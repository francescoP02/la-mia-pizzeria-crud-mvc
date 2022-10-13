using la_mia_pizzeria_static;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> pizzasList { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public PizzaContext()
        {

        }

        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-pizza;Integrated Security=True");
        }
    }
}
