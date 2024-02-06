using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CS_MongoDB_Recipe_API.DAO;
using CS_MongoDB_Recipe_API.Models;

namespace MongoDBRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetRecipe : ControllerBase
    {
        private MongoDBClient mongoDBClient;

        public GetRecipe()
        {
            mongoDBClient = MongoDBClient.GetInstance();
        }

        [HttpGet("{title}")] 
        public Recipe? GetRecipeByTitle(string title)
        {
            var result = mongoDBClient.GetDocument("Recipes", title);

            if (result != null)
            {
                Recipe recipe = new Recipe(result.RecipeTitle, result.RecipeDescription);

                return recipe;
            }
            else
            {

                return null;
            }
        }

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
