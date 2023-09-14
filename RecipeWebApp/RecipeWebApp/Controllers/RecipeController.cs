using Microsoft.AspNetCore.Mvc;
using RecipeWebApp.DTO;
using RecipeWebApp.Services;

namespace RecipeWebApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
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

        public IActionResult ViewRecipe(string id)
        {
            var recipe = recipeService.GetRecipeById(id);

            return View(recipe);
        }

        public IActionResult Change(string id)
        {
            var recipe = recipeService.RetrieveInfoForChangeRecipe(id);

            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Change(string id, ChangeRecipeFormModel model) 
        {
            await this.recipeService.ChangeRecipe(id, model);

            return RedirectToAction("ViewRecipe", new { id = id});
        }
    }
}
