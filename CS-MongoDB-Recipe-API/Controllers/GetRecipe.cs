using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CS_MongoDB_Recipe_API.DAO;
using CS_MongoDB_Recipe_API.Models;
using Microsoft.AspNetCore.Authorization;
using CS_MongoDB_Recipe_API.Services.Interfaces;
using CS_MongoDB_Recipe_API.Services;

namespace MongoDBRecipeApp.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    /** An API endpoint controller that provides a method for getting all documents from a collection and a method for getting a specific
     * document from a collection based on title
     */
    public class GetRecipe : ControllerBase
    {
        private readonly IRecipeService recipeService;

        public GetRecipe()
        {
            recipeService =  RecipeService.GetInstance();
        }

        //This method returns an recipe based on title
        [HttpGet("{title}")] 
        public IActionResult GetRecipeByTitle(string title)
        {
            var result = recipeService.GetDocument("Recipes", title);

            if (result != null)
            {
                Recipe recipe = new Recipe(result.RecipeTitle, result.RecipeDescription, result.RecipeId);

                return Ok(recipe);
            }
            else
            {

                return NotFound();
            }
        }

        //This method returns all recipes from
        [HttpGet("All")]
        public List<Recipe> GetAllRecipes() {

            var result = recipeService.GetAllFromCollection("Recipes");

            if(result != null) {
                return result;
             } else
            {
                return null;
            }
            
        }
        [HttpGet("Random")]
        public Recipe GetARandomRecipe()
        {   
             var listOfRecipes = recipeService.GetAllFromCollection("Recipes");

             var random = new Random();
              
             var randomRecipe = random.Next(listOfRecipes.Count);

            return listOfRecipes[randomRecipe];


        }
        
    }
}
