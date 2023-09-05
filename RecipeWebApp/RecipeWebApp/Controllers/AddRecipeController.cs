using Microsoft.AspNetCore.Mvc;
using RecipeWebApp.DTO;
using RecipeWebApp.Services;

namespace RecipeWebApp.Controllers
{
    public class AddRecipeController : Controller
    {
        private readonly IRecipeService recipeService;

        public AddRecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRecipeFormSubmit model)
        {

            if (!ModelState.IsValid) 
            {
                return View();
            }

            await recipeService.AddRecipe(model);

            return RedirectToAction("Index","Home");

        }
    }
}
