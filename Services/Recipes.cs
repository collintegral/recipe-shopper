using System.Text.Json;
using RecipeShopper.Models;


namespace RecipeShopper.Services
{
    public class RecipeService
    {
        private readonly HttpClient _http;
        public RecipeService(HttpClient http)
        {
            _http = http;
        }

        private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            var json = await _http.GetStringAsync("recipes.json");

            return JsonSerializer.Deserialize<List<Recipe>>(json, options);
        }

        public async Task<Recipe?> GetRecipeAsync(int id)
        {
            var recipes = await GetRecipesAsync();
            if (id >= 0 && id < recipes.Count) return recipes[id];
            else return null;
        }
    }
}