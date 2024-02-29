using Newtonsoft.Json;
using RecipeWebApp.Models;
using RecipeWebApp.Services.ServiceInterfaces;
namespace RecipeWebApp.Services
{
    public class RecipesService : IRecipeService
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
                var response = await _httpClient.GetAsync("http://localhost:5291/api/GetRecipe/All");
                var content = await response.Content.ReadAsStringAsync();
                var parsedResponse = JsonConvert.DeserializeObject<List<Recipe>>(content);
                if(parsedResponse != null)
                {
                    return parsedResponse;
                }

                return null;
            } catch(Exception e)
            { 
                Console.Write($"Error getting all recipes: {e}");
                throw new Exception($"Error getting all recipes: {e}");
            }
        }

        public async Task<Recipe> getARecipeByTitle(string title)
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
                Console.Write($"Error getting recipe with id {title}: {e}");
                throw new Exception($"Error getting recipe with id {title}: {e}");
            }
        }
    }
}
