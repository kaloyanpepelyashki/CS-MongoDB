using CS_MongoDB_Recipe_API.Models;
using MongoDB.Driver;

namespace CS_MongoDB_Recipe_API.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserIdent?> AuthSignIn(String userEmail, String userPassword);
        public Task<Boolean> AuthSignUp(String userEmail, String userPassword);


    }
}
