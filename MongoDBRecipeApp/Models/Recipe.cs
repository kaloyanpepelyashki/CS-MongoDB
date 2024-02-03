using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBRecipeApp.Models
{
    public class Recipe
    {
        [BsonElement("recipeId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("title")]
        public string RecipeTitle { get; } = " ";
        public string RecipeDescription { get; } = " ";

        public Recipe(string title, string description)
        {
            RecipeTitle= title;
            RecipeDescription= description;
        }


    }
}
        