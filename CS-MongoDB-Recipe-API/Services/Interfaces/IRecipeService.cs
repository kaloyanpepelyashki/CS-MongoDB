using CS_MongoDB_Recipe_API.Models;

namespace CS_MongoDB_Recipe_API.Services.Interfaces
{
    public interface IRecipeService
    {
        public Recipe? GetDocument(string collectionName, string title);
        public List<Recipe> GetAllFromCollection(string collectionName);
        public Task<bool> InsertDocument(Recipe recipe);
        public Task<bool> DeleteDocumentByTitle(string title);
        public Task<bool> DeleteDocumentById(string recipeId);
        public Task<bool> UpdateRecipeById(string recipeId, string newRecipeTitle, string newRecipeDescription);
    }
}
