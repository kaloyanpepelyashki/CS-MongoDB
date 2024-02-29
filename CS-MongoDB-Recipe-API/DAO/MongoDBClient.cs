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

    }
}
