namespace RecipeWebApp.Models
{
    public class Recipe
    {
     
        private string Id { get; init; }
        public string RecipeTitle { get; init; }
        public string RecipeDescription { get; init; }
        public Recipe(string title, string description)
        {
            RecipeTitle = title;
            RecipeDescription = description;
        }



    }
}
