using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDBRecipeApp.Models;
using MongoDBRecipeApp.DAO;

namespace MongoDBRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateRecipeController : ControllerBase
    {
        private readonly MongoDBClient mongoDBClient;
        public CreateRecipeController()
        {


            MongoDBClient mongoDbClient = MongoDBClient.GetInstance();
        }


        [HttpPost]
        public IActionResult Post([FromBody] RecipeDto recipeDto)
        {
            if (recipeDto == null)
            {
                return BadRequest();
            }

            Recipe newRecipe = new Recipe(recipeDto.Title, recipeDto.Description);

            mongoDBClient.InsertDocument("Recipes", newRecipe);

            return Ok();

        }

        public class RecipeDto
        {
            public string Title { get; set; }
            public string Description { get; set; }
        }
    }
}