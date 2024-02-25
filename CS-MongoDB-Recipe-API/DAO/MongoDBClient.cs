using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using CS_MongoDB_Recipe_API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CS_MongoDB_Recipe_API.DAO

{
    /** This is the singleton class to access and interact with the MongoDB client
     * The class provides methods for reading from the MongoDB database and writing to the MongoDB database
     */
    public class MongoDBClient
    {
        protected static MongoDBClient instance;
        protected readonly IConfiguration _configuration;
        protected readonly IMongoDatabase? _database;
        protected readonly string? connectionString;
        public IMongoClient client;


        public MongoDBClient()
        {
            try
            {
                
                _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
                //The connection string is stored as a environmental variable in the appsettings.json file
                connectionString = _configuration.GetValue<string>("MongoDB:DBConnectionString"); 
                var databaseName = Environment.GetEnvironmentVariable("DatabaseName");
                client = new MongoClient(connectionString);
                _database = client.GetDatabase("Recipes");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception initialising MMongoDBClient: {e}");
            }
        }

        public static MongoDBClient GetInstance()
        {
            if (instance == null)
            {
                instance = new MongoDBClient();
            }
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
        public async Task<bool> InsertDocument (Recipe recipe)
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
                return false;
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
    }
}
