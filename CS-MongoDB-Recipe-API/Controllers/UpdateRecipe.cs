using CS_MongoDB_Recipe_API.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CS_MongoDB_Recipe_API.Controllers
{
    public class UpdateRecipe : Controller
    {
        private readonly MongoDBClient mongoDBClient;
        public UpdateRecipe()
        {
            mongoDBClient = MongoDBClient.GetInstance();
        }


    }
}
