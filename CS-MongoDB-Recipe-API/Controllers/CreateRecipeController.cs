using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CS_MongoDB_Recipe_API.Models;
using CS_MongoDB_Recipe_API.DAO;

namespace MongoDBRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /** An API endpoint controller that handles the creation of a new recipe in the MongoDB databse */
    public class CreateRecipeController : ControllerBase
    {
        private readonly MongoDBClient mongoDBClient;
        public CreateRecipeController()
        {
            mongoDBClient = MongoDBClient.GetInstance();
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RecipeDto recipeDto)
        {
            if (recipeDto == null)
            {
                return BadRequest();
            }

            Recipe newRecipe = new Recipe(recipeDto.RecipeTitle, recipeDto.RecipeDescription);

            var insertResult = await mongoDBClient.InsertDocument(newRecipe);

            if (insertResult)
            {
                return Ok();
            } else
            {
                return BadRequest();
            }

        }

        public class RecipeDto
        {
            public string RecipeTitle { get; set; }
            public string RecipeDescription { get; set; }
        }
    }
}