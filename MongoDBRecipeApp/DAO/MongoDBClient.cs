using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using MongoDBRecipeApp.Models;

namespace MongoDBRecipeApp.DAO

{
    /** This is the singleton class to access and interact with the MongoDB client
     * The class provides methods for reading from the MongoDB database and writing to the MongoDB database
     */
    public class MongoDBClient
    {
        private static MongoDBClient instance;

        private readonly IMongoDatabase? _database;
        private readonly string? connectionString;
        public IMongoClient client; 


        public MongoDBClient()
        {
            try
            {

                // connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                connectionString = "mongodb+srv://kaloyanpepelyashki:eVTnDFaZCXynFnOk@recipeappcluster.trppcrn.mongodb.net/?retryWrites=true&w=majority";
                var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

                client = new MongoClient(connectionString);
                _database = client.GetDatabase("Recipes");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception initialising MMongoDBClient: {e}");
            }
        }

        public static MongoDBClient GetInstance()
        {
              if(instance == null)
            {
                instance = new MongoDBClient();
            }
            return instance;
        }

        public Recipe? GetDocument(string collectionName, string title)
        {
            try
            {
                var collection = _database?.GetCollection<Recipe>(collectionName);
                var filter = Builders<Recipe>.Filter.Eq("title", title);
                var document = collection.Find(filter).First();

                if (document != null)
                {
                    return document;
                }
                else
                {
                    return null;
                }
            } catch(Exception e)
            {
                Console.WriteLine($"Error getting {title} {e}");
                throw new Exception($"Error getting data from {collectionName}: {e}");
            }
        }

        public List<BsonDocument> GetAllFromCollection(string collectionName)
        {
            try
            {
                var collection = _database?.GetCollection<BsonDocument>(collectionName);
                var result = collection.Find(_ => true);

                return result.ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Error getting items for {collectionName}: {e}");
            }
        }
        public async void InsertDocument(string collectionName, Recipe recipe)
        {
            try
            {
                var collection = _database?.GetCollection<Recipe>(collectionName);
                await collection?.InsertOneAsync(recipe);
                Console.WriteLine("Inserted into Database");
            } catch(Exception e)
            {
                Console.WriteLine($"Error inserting document in {collectionName}: {e}");
            }

        }
    }
}
