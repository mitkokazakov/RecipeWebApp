using Microsoft.AspNetCore.Mvc;

namespace RecipeWebApp.Controllers
{
    public class AddRecipeController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}
