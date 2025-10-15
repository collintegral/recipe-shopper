using RecipeShopper.Models;

namespace RecipeShopper.Models
{
    public class Recipe(int id, string name, string summary, List<Ingredient> ingredients, int servings, List<string> steps)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Summary { get; set; } = summary;
        public List<Ingredient> Ingredients { get; set; } = ingredients;
        public int Servings { get; set; } = servings;
        public List<string> Steps { get; set; } = steps;
    }
}