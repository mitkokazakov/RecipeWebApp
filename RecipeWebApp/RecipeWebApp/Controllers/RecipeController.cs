using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeWebApp.DTO;
using RecipeWebApp.Services;
using System.Data;

namespace RecipeWebApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(AddRecipeFormSubmit model)
        {

            if (!ModelState.IsValid) 
            {
                return View();
            }

            await recipeService.AddRecipe(model);

            TempData["Success"] = "Recipe has been added successfully!";

            return RedirectToAction("Index","Home");

        }

        public IActionResult ViewRecipe(string id)
        {
            var recipe = recipeService.GetRecipeById(id);

            return View(recipe);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Change(string id)
        {
            var recipe = recipeService.RetrieveInfoForChangeRecipe(id);

            return View(recipe);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Change(string id, ChangeRecipeFormModel model) 
        {
            await this.recipeService.ChangeRecipe(id, model);

            return RedirectToAction("ViewRecipe", new { id = id});
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id) 
        {
            await this.recipeService.DeleteRecipe(id);

            return RedirectToAction("Index","Home");
        }

        public IActionResult RecipeByCategory(string id) 
        {
            ViewBag.CurrentCategory = id;

            var recipes = recipeService.GetRecipesByCategory(id);

            return View(recipes);
        }
    }
}
