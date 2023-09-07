using Microsoft.AspNetCore.Mvc;
using RecipeWebApp.Models;
using RecipeWebApp.Services;
using System.Diagnostics;

namespace RecipeWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeService recipeService;

        public HomeController(ILogger<HomeController> logger, IRecipeService recipeService)
        {
            _logger = logger;
            this.recipeService = recipeService;
        }

        public IActionResult Index()
        {
            var sixRecipes = this.recipeService.GetFirstSixRecipes();

            return View(sixRecipes);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}