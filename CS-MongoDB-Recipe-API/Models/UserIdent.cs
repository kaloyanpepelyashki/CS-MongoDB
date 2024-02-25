using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CS_MongoDB_Recipe_API.Models
{
    public class UserIdent
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId UserId { get; init; }
        [BsonElement("email")]
        public string Email { get; init; }
        [BsonElement("password")]
        public string Password { get; init; }

        public UserIdent(String userEmail, String userPassword)
        {
            Email = userEmail;
            Password = userPassword;
        }
    }
}
