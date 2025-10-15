using System.Text.Json;
using RecipeShopper.Components.Pages;
using RecipeShopper.Models;


namespace RecipeShopper.Services
{
    public class RecipeService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string path;

        public RecipeService(IWebHostEnvironment env)
        {
            _env = env;
            path = Path.Combine(_env.WebRootPath, "recipes.json");
        }

        private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            var json = await File.ReadAllTextAsync(path);

            return JsonSerializer.Deserialize<List<Recipe>>(json, options);
        }

        public async Task<List<Recipe>> GetRecipesInCartAsync()
        {
            var json = await File.ReadAllTextAsync(path);
            List<Recipe> recipes = new();
            foreach (Recipe recipe in JsonSerializer.Deserialize<List<Recipe>>(json, options))
            {
                if (recipe.InCart) recipes.Add(recipe);
            }
            return recipes;
        }

        public async Task<Recipe?> GetRecipeAsync(int id)
        {
            var recipes = await GetRecipesAsync();
            if (id >= 0 && id < recipes.Count)
            {
                var recipe = recipes[id];

                recipe.Ingredients ??= new List<Ingredient>();
                if (recipe.Ingredients.Count == 0)
                    recipe.Ingredients.Add(new Ingredient("", "", 0));

                recipe.Steps ??= new List<string>();
                if (recipe.Steps.Count == 0)
                    recipe.Steps.Add("");

                return recipe;
            }
            else return null;
        }

        public async Task<int> GetRecipeCountAsync()
        {
            var json = await File.ReadAllTextAsync(path);
            var recipes = JsonSerializer.Deserialize<List<Recipe>>(json, options);
            return recipes.Count;
        }

        public async Task FlipRecipeCartAsync(int id)
        {
            var recipes = await GetRecipesAsync();

            if (id >= 0 && id < recipes.Count)
            {
                recipes[id].InCart = !recipes[id].InCart;
                var updatedRecipes = JsonSerializer.Serialize(recipes, options);
                await File.WriteAllTextAsync(path, updatedRecipes);
            }
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var recipes = await GetRecipesAsync();

            if (recipe.Id >= 0 && recipe.Id < recipes.Count)
            {
                recipes[recipe.Id] = recipe;
                var updatedRecipes = JsonSerializer.Serialize(recipes, options);
                await File.WriteAllTextAsync(path, updatedRecipes);
            }
        }
        
        public async Task NewRecipeAsync(Recipe recipe)
        {
            var recipes = await GetRecipesAsync();

            recipes.Add(recipe);
            var updatedRecipes = JsonSerializer.Serialize(recipes, options);
            await File.WriteAllTextAsync(path, updatedRecipes);
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipes = await GetRecipesAsync();

            if (id >= 0 && id < recipes.Count)
            {
                recipes.RemoveAt(id);
                var updatedRecipes = JsonSerializer.Serialize(recipes, options);
                await File.WriteAllTextAsync(path, updatedRecipes);
            }
        }
    }
}