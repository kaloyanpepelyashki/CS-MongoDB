using CS_MongoDB_Recipe_API.DAO;
using CS_MongoDB_Recipe_API.Models;
using CS_MongoDB_Recipe_API.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CS_MongoDB_Recipe_API.Services
{
    public class RecipeService : IRecipeService
    {
        private static RecipeService instance;
        protected IMongoClient _mongoDbClient;
        protected IMongoCollection<Recipe> _usersCollection;
        protected IMongoDatabase _database;

        protected RecipeService()
        {
            var mongoDB = MongoDBClient.GetInstance();
            _mongoDbClient = mongoDB.client;
            _database = _mongoDbClient.GetDatabase("Recipes");
        }

        public static RecipeService GetInstance()
        {
            instance ??= new RecipeService();
            return instance;
        }


        /** The method return a document from a collection based on title and collection name. The method takes in two parameters 
         * The method takes in two string parameters "collectionName" and "title".
         */
        public Recipe? GetDocument(string collectionName, string title)
        {
            try
            {
                var collection = _database?.GetCollection<Recipe>(collectionName);
                var filter = Builders<Recipe>.Filter.Eq("RecipeTitle", title);
                var document = collection.Find(filter).First();

                if (document == null)
                {
                    return null;
                }
                else
                {
                    return document;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting {title} {e}");
                throw new Exception($"Error getting data from {collectionName}: {e}");
            }
        }

        /** This method gets all documents from a collection based on collection name
         * The method takes in one string parameter "collectionName"
         */
        public List<Recipe> GetAllFromCollection(string collectionName)
        {
            try
            {
                var collection = _database?.GetCollection<Recipe>(collectionName);
                var result = collection.Find(new BsonDocument()).ToList();

                if (result.Count == 0)
                {
                    return null;
                }

                return result;

            }
            catch (Exception e)
            {
                throw new Exception($"Error getting items for {collectionName}: {e}");
            }
        }
        /** This method inserts a document in a collection.
         * The method takes in a recipe object, to be inserted into the MongoDB.
         */
        public async Task<bool> InsertDocument(Recipe recipe)
        {
            try
            {
                var collection = _database?.GetCollection<Recipe>("Recipes");
                await collection?.InsertOneAsync(recipe);
                Console.WriteLine("Inserted into Database");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error inserting document in Recipes: {e}");
                throw new Exception($"Error inserting document: {e}");
            }

        }
        /** This method inserts a document in a collection.
         * The method takes in a string parameter, to be inserted into the MongoDB.
         */
        public async Task<bool> DeleteDocumentByTitle(string title)
        {
            try
            {
                var collection = _database?.GetCollection<Recipe>("Recipes");
                var filter = Builders<Recipe>.Filter.Eq("RecipeTitle", title);
                await collection?.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting document from Recipes: {e}");
                return false;
            }
        }

        public async Task<bool> DeleteDocumentById(string recipeId)
        {
            try
            {
                var collection = _database?.GetCollection<Recipe>("Recipes");
                var filter = Builders<Recipe>.Filter.Eq("RecipeId", recipeId);
                await collection?.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting document from Recipes: {e}");
                throw new Exception($"Error deleting recipe with id {recipeId}: {e}");
            }
        }

        public async Task<bool> UpdateRecipeById(string recipeId, string newRecipeTitle, string newRecipeDescription)
        {
            try
            {
                var collection = _database?.GetCollection<Recipe>("Recipes");
                var filter = Builders<Recipe>.Filter.Eq("RecipeId", recipeId);
                var patch = Builders<Recipe>.Update.Set("RecipeTitle", newRecipeTitle).Set("RecipeDescription", newRecipeDescription);
                var updateResult = await collection.UpdateOneAsync(filter, patch);

                return updateResult.IsAcknowledged;
            }
            catch (Exception e)
            {
                throw new Exception($"Error updating recipe with id {recipeId}: {e}");
            }
        }
    }
}
