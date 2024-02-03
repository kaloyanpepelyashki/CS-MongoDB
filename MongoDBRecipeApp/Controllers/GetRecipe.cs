using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDBRecipeApp.DAO;
using MongoDBRecipeApp.Models;

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

            if(result != null)
            {
                Recipe recipe = new Recipe(result.RecipeTitle, result.RecipeDescription);

                return recipe;
            }

            return null;
        }
        
    }
}
