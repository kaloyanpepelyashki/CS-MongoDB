using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CS_MongoDB_Recipe_API.Models;
using CS_MongoDB_Recipe_API.DAO;

namespace MongoDBRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateRecipeController : ControllerBase
    {
        private MongoDBClient mongoDBClient;
        public CreateRecipeController()
        {


            mongoDBClient = MongoDBClient.GetInstance();
        }


        [HttpPost]
        public IActionResult Post([FromBody] RecipeDto recipeDto)
        {
            if (recipeDto == null)
            {
                return BadRequest();
            }

            Recipe newRecipe = new Recipe(recipeDto.RecipeTitle, recipeDto.RecipeDescription);

            mongoDBClient.InsertDocument("Recipes", newRecipe);

            return Ok();

        }

        public class RecipeDto
        {
            public string RecipeTitle { get; set; }
            public string RecipeDescription { get; set; }
        }
    }
}