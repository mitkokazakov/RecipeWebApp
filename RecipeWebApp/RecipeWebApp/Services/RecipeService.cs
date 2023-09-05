using RecipeWebApp.DTO;
using RecipeWebApp.Data.Models;

namespace RecipeWebApp.Services
{
    public class RecipeService : IRecipeService
    {
        public Task AddRecipe(AddRecipeFormSubmit model)
        {
            string extension = Path.GetExtension(model.Image.FileName);

            Image image = new Image()
            {
                Name = Guid.NewGuid().ToString(),
                Extension = extension
            };

            var ingredientsArray = SplitIngredients(model.AllIngredients);

            Recipe recipe = new Recipe()
            {
                Name = model.RecipeName,
                Instructions = model.Instructions,
                Image = image,
                Category = model.Category,
                CookingTime = model.CookingTime,
                Servings = model.Servings,
                CreatedOn = DateTime.UtcNow,
                Ingridients = ingredientsArray[0],
            };
        }

        private string[] SplitIngredients(string allIngredients) 
        {
            var arr = allIngredients.Split("\r\n\r\n");

            return arr;
        }

        private List<SubIngredient> SubIngredientsSplit(string[] allIngredients) 
        {
            List<SubIngredient> subIngredients = new List<SubIngredient>();

            foreach (var subIngr in allIngredients.Skip(1))
            {

            }

            return subIngredients;
        }
    }
}
