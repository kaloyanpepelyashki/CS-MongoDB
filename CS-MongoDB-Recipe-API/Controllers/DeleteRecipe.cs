using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using CS_MongoDB_Recipe_API.DAO; 

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteRecipe : ControllerBase
    {
        private readonly MongoDBClient mongoDBClient;
        
        public DeleteRecipe()
        {
            mongoDBClient = MongoDBClient.GetInstance();
        }

        [HttpDelete("{title}")]
        public void DeleteRecipeByTitle(string title)
        {
            mongoDBClient.DeleteDocumentByTitle("Recipes", title); 
        }
    }
}
