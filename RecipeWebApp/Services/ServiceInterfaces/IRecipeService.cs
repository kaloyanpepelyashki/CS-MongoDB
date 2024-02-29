using RecipeWebApp.Models;

namespace RecipeWebApp.Services.ServiceInterfaces
{
    public interface IRecipeService
    {
        Task<List<Recipe>> getAllRecipes();
        Task<Recipe> getARecipeByTitle(string title);


    }
}
