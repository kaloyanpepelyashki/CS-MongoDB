using MongoDB.Driver;
using MongoDBRecipeApp.DAO;
using MongoDBRecipeApp.Models;

  var builder = WebApplication.CreateBuilder(args);

 var app = builder.Build();

 //Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
app.UseExceptionHandler("/Home/Error");
 //The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
 app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
/*
app.UseAuthorization();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");
*/
var mongoDBClient = MongoDBClient.GetInstance();

//Console.WriteLine(mongoDBClient.GetDocument("Recipes", "Cool Recipe 1"));
Console.WriteLine("<=====================>");
Console.WriteLine(mongoDBClient.GetDocument("Recipes", "Sweet fish"));
Recipe? returnedRecipe = new Recipe(mongoDBClient.GetDocument("Recipes", "Sweet fish").RecipeTitle, mongoDBClient.GetDocument("Recipes", "Sweet fish").RecipeDescription);

Console.WriteLine(returnedRecipe.RecipeTitle);

Console.WriteLine("<=====================>");
app.Run();

