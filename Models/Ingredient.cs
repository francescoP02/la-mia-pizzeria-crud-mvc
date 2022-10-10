using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("ingredients")]
    public class Ingredient
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Pizza>? pizzas { get; set; }

        public Ingredient()
        {

        }
    }
}
