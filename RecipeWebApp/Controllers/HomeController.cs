using Microsoft.AspNetCore.Mvc;
using RecipeWebApp.Models;
using RecipeWebApp.Services.ServiceInterfaces;
using System.Diagnostics;

namespace RecipeWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeService _recipeService;

        public HomeController(ILogger<HomeController> logger, IRecipeService recipeService)
        {   
            _recipeService = recipeService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var recipesResponse = await _recipeService.getAllRecipes();
            if (recipesResponse == null)
            {
                ViewData["recipes"] = null;
                return View();
            }
        
                ViewData["recipes"] = recipesResponse;
                return View();
            
        }

        public IActionResult CreateRecipe()
        {
            return View();
        }

        public IActionResult UpdateRecipe(int id)
        {
            return View();
        }

        public IActionResult DeleteRecipe()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}