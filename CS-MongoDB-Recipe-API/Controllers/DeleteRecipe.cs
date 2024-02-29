using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;
using CS_MongoDB_Recipe_API.Services.Interfaces;
using CS_MongoDB_Recipe_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    /** An API endpoint controller that handles the deletion of a recipe in the MongoDB databse */
    public class DeleteRecipe : ControllerBase
    {
        private readonly IRecipeService recipeService;

        public DeleteRecipe()
        {
            recipeService = RecipeService.GetInstance();
        }

        [HttpDelete("byTitle/{title}")]
        public async Task<IActionResult> DeleteRecipeByTitle(string title)
        {
            try
            {
                var deleteionResult = await recipeService.DeleteDocumentByTitle(title);

                if (deleteionResult)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            } catch (Exception e)
            {
                return BadRequest($"Error deleting recipe: {e}");
            }
        }
        [HttpDelete("byId/{id}")]
        public async Task<IActionResult> DeleteRecipeById(string id)
        {
            try
            {
                var deleteionResult = await recipeService.DeleteDocumentById(id);
                if (deleteionResult)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            } catch (Exception e)
            {
                return BadRequest($"Error deleting recipe: {e}");
            }
        }
    }
}
