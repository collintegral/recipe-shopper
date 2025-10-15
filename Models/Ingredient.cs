namespace RecipeShopper.Models
{
    public class Ingredient(string name, string unit, double quantity)
    {
        public string Name { get; set; } = name;
        public string Unit { get; set; } = unit;
        public double Quantity { get; set; } = quantity;
    }
}