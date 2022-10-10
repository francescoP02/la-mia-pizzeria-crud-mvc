namespace la_mia_pizzeria_static.Models
{
    public class PizzaCategory
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<int> selectedIngredients { get; set; }

        public PizzaCategory()
        {
            Ingredients = new List<Ingredient>();
        }
        
    }
}
