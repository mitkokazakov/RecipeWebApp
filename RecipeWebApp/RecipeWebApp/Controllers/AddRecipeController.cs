using Microsoft.AspNetCore.Mvc;
using RecipeWebApp.DTO;

namespace RecipeWebApp.Controllers
{
    public class AddRecipeController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddRecipeFormSubmit model)
        {
            return RedirectToAction("Index","Home");
        }
    }
}
