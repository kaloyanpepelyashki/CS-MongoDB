using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CS_MongoDB_Recipe_API.DAO;
using CS_MongoDB_Recipe_API.Models;

namespace MongoDBRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /** An API endpoint controller that provides a method for getting all documents from a collection and a method for getting a specific
     * document from a collection based on title
     */
    public class GetRecipe : ControllerBase
    {
        private readonly MongoDBClient mongoDBClient;

        public GetRecipe()
        {
            mongoDBClient = MongoDBClient.GetInstance();
        }

        //This method returns an recipe based on title
        [HttpGet("{title}")] 
        public IActionResult GetRecipeByTitle(string title)
        {
            var result = mongoDBClient.GetDocument("Recipes", title);

            if (result != null)
            {
                Recipe recipe = new Recipe(result.RecipeTitle, result.RecipeDescription);

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

            var result = mongoDBClient.GetAllFromCollection("Recipes");

            if(result != null) {
                return result;
             } else
            {
                return null;
            }
            
        }
        
    }
}
