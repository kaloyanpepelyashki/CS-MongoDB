using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CS_MongoDB_Recipe_API.Models
{
    public class Recipe
    {
        [BsonElement("_Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        private ObjectId Id { get; }
        [BsonElement("RecipeTitle")]
        public string RecipeTitle { get; }
        [BsonElement("RecipeDescription")]
        public string RecipeDescription { get; }

        public Recipe(string title, string description)
        {
            RecipeTitle = title;
            RecipeDescription = description;
        }



    }
}
