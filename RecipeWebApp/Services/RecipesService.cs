using Newtonsoft.Json;
using RecipeWebApp.Models;
namespace RecipeWebApp.Services
{
    public class RecipesService
    {
        private readonly HttpClient _httpClient;

        public RecipesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Recipe>> getAllRecipes()
        {
            try
            {
                var response = await _httpClient.GetAsync("url/endpoint");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Recipe>>(content);
            } catch(Exception e)
            {
                Console.Write($"Error getting all recipes: {e}");
                throw new Exception($"Error getting all recipes: {e}");
            }
        }

        public async Task<Recipe> getARecipeById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync("url/endpoint/{id}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Recipe>(content);
            }
            catch (Exception e)
            {
                Console.Write($"Error getting recipe with id {id}: {e}");
                throw new Exception($"Error getting recipe with id {id}: {e}");
            }
        }
    }
}
