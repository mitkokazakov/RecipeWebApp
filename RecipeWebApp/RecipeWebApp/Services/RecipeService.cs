using RecipeWebApp.DTO;
using RecipeWebApp.Data.Models;
using RecipeWebApp.Data;

namespace RecipeWebApp.Services
{
    public class RecipeService : IRecipeService
    {

        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment hostEnvironment;

        public RecipeService(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            this.db = db;
            this.hostEnvironment = hostEnvironment; 
        }

        public async Task AddRecipe(AddRecipeFormSubmit model)
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
                ImageId = image.Id,
                Category = model.Category,
                CookingTime = model.CookingTime,
                Servings = model.Servings,
                CreatedOn = DateTime.UtcNow,
                Ingridients = ingredientsArray[0],
            };

            if (ingredientsArray.Count() > 1)
            {
                recipe.SubIngredients = ingredientsArray[1];
            }

            await db.Recipies.AddAsync(recipe);
            await db.SaveChangesAsync();

            SavePicture(model.Image,image.Id);
        }

        public IEnumerable<RecipeCoverViewModel> GetFirstSixRecipes()
        {
            var firstSix = db.Recipies.OrderByDescending(r => r.CreatedOn).Take(6).Select(r => new RecipeCoverViewModel() 
            {
                Id = r.Id,
                Category = r.Category.ToUpper(),
                Name = r.Name,
                ImagePath = r.ImageId + r.Image.Extension
            })
                .ToList();

            return firstSix;
        }

        public SingleRecipeViewModel DisplayRecipe(string recipeId) 
        {
            var searchedRecipe = db.Recipies.FirstOrDefault(r => r.Id == recipeId);

            var recipeViewModel = new SingleRecipeViewModel()
            {
                Name = searchedRecipe.Name,
                Category = searchedRecipe.Category,
                ImagePath = searchedRecipe.ImageId + searchedRecipe.Image.Extension,
                Ingredients = searchedRecipe.Ingridients,
                SubIngredients = searchedRecipe.SubIngredients,
                Instructions = searchedRecipe.Instructions,
                CookingTime = searchedRecipe.CookingTime,
                Servings = searchedRecipe.Servings
            };

            return recipeViewModel;
        }

        private string[] SplitIngredients(string allIngredients) 
        {
            var arr = allIngredients.Split("\r\n\r\n",2);

            return arr;
        }

        private void SavePicture(IFormFile image, string pictureName)
        {
            string uploadsFolder = Path.Combine(this.hostEnvironment.WebRootPath, "recipes");

            string extension = Path.GetExtension(image.FileName);

            string pictureFileName = pictureName + extension;

            string filePath = Path.Combine(uploadsFolder, pictureFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

        }

        
    }
}
