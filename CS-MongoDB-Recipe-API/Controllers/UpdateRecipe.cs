using CS_MongoDB_Recipe_API.Services;
using CS_MongoDB_Recipe_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CS_MongoDB_Recipe_API.Controllers
{
    [ApiController]
    public class UpdateRecipe : Controller
    {
        private readonly IRecipeService recipeService;
        public UpdateRecipe()
        {
            recipeService = RecipeService.GetInstance();
        }

        [HttpPatch("byId")]
        public async Task<IActionResult> UpdateRecipeById([FromBody] RecipeUpdateDTO recipeUpdateDto)
        {
            try
            {
                await recipeService.UpdateRecipeById(recipeUpdateDto.TargetRecipeId, recipeUpdateDto.NewRecipeTitle, recipeUpdateDto.NewRecipeDescription);
                return Ok();

            } catch(Exception e)
            {
                return BadRequest($"Error inserting recipe: {e}");
            }
        }


        public class RecipeUpdateDTO
        {
            public string TargetRecipeId { get; set; }
            public string NewRecipeTitle { get; set; }
            public string NewRecipeDescription { get; set;}
        }
    }
}
