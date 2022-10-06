using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using la_mia_pizzeria_static.Validation;

namespace la_mia_pizzeria_static.Models
{
    [Table("pizzas")]
    public class Pizza
    {
        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = "Il campo nome è obbligatorio")]
        [StringLength(25, ErrorMessage = "Il nome non può avere più di 25 caratteri")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Il campo descrizione è obbligatorio")]
        [StringLength(255, ErrorMessage = "Il nome non può avere più di 255 caratteri")]
        [MoreThanFiveWords]
        public string Description { get; set; }

        [Required(ErrorMessage = "Il campo foto è obbligatorio")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Il campo prezzo è obbligatorio")]
        [NegativeOrZero]
        public float Price { get; set; }

        public Pizza()
        {

        }

        public Pizza(string name, string description, string photo, float price)
        {
            Name = name;
            Description = description;
            Photo = photo;
            Price = price;
        }
    }
}
