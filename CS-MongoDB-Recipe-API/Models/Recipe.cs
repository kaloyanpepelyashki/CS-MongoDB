using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CS_MongoDB_Recipe_API.Models
{
    public class Recipe
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        private ObjectId Id { get; init; }
        [BsonElement("RecipeTitle")]
        public string RecipeTitle { get; init; }
        [BsonElement("RecipeDescription")]
        public string RecipeDescription { get; init; }

        public Recipe(string title, string description)
        {
            RecipeTitle = title;
            RecipeDescription = description;
        }



    }
}
