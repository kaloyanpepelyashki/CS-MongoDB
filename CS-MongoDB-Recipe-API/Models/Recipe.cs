using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CS_MongoDB_Recipe_API.Models
{
    public class Recipe
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; init; }

        [BsonElement("RecipeId")]
        public string RecipeId { get; init; }
        [BsonElement("RecipeTitle")]
        public string RecipeTitle { get; init; }
        [BsonElement("RecipeDescription")]
        public string RecipeDescription { get; init; }

        public Recipe(string title, string description, string Id)
        {
            RecipeId = Id;
            RecipeTitle = title;
            RecipeDescription = description;
        }


    }
}
