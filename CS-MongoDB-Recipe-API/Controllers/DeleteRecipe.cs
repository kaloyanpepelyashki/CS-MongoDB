using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CS_MongoDB_Recipe_API.DAO; 

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /** An API endpoint controller that handles the deletion of a recipe in the MongoDB databse */
    public class DeleteRecipe : ControllerBase
    {
        private readonly MongoDBClient mongoDBClient;
        
        public DeleteRecipe()
        {
            mongoDBClient = MongoDBClient.GetInstance();
        }

        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteRecipeByTitle(string title)
        {
            var deleteionResult = await mongoDBClient.DeleteDocumentByTitle(title); 

            if(deleteionResult)
            {
                return Ok();
            } else
            {
                return NotFound();
            }
        }
    }
}
