using CS_MongoDB_Recipe_API.DAO;
using CS_MongoDB_Recipe_API.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace CS_MongoDB_Recipe_API.Services
{
    public class UserService
    {
        protected static UserService instance;
        protected IMongoClient _mongoDbClient;
        protected IMongoCollection<UserIdent> _usersCollection;
        protected IMongoDatabase _database;
 
        protected UserService()
        {
            var mongoDb = MongoDBClient.GetInstance();
            _mongoDbClient = mongoDb.client;
            _database = _mongoDbClient.GetDatabase("Users");
            _usersCollection = _database.GetCollection<UserIdent>("UserData");
        }

        public static UserService GetInstance()
        {
            instance ??= new UserService();
            return instance;
        }

        public async Task<UserIdent?> AuthSignIn(String userEmail, String userPassword)
        {
            try
            {
                return await _usersCollection.Find(user => user.Email == userEmail && user.Password == userPassword).FirstOrDefaultAsync();

            }catch(Exception e)
            {
                Console.WriteLine($"Error in authentication SignIn: {e}");
                return null;
            }
        }

        public async Task<Boolean> AuthSignUp(String userEmail, String userPassword)
        {
            try
            {
                var profilesMatchingCreds = await _usersCollection.Find(user => user.Email == userEmail && user.Password == userPassword).FirstOrDefaultAsync();

                if (profilesMatchingCreds == null)
                {
                    var newUserProfile = new UserIdent(userEmail, userPassword);
                    await _usersCollection.InsertOneAsync(newUserProfile);
                    return true;
                }

                return false;
            } catch(Exception e)
            {
                Console.WriteLine($"Error signing up: {e}");
                throw new Exception($"Error signing user up: {e}");
            }
        }
    }
}   
