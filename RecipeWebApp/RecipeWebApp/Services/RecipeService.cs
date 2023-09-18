using RecipeWebApp.DTO;
using RecipeWebApp.Data.Models;
using RecipeWebApp.Data;
using System.Runtime.CompilerServices;

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
                Category = CapitalizeOnlyFirstLetterOfCategoryName(model.Category),
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
            var firstSix = db.Recipies.Where(r => r.IsDeleted == false).OrderByDescending(r => r.CreatedOn).Take(6).Select(r => new RecipeCoverViewModel() 
            {
                Id = r.Id,
                Category = r.Category.ToUpper(),
                Name = r.Name,
                ImagePath = r.ImageId + r.Image.Extension
            })
                .ToList();

            return firstSix;
        }

        public SingleRecipeViewModel GetRecipeById(string recipeId) 
        {
            var searchedRecipe = db.Recipies.FirstOrDefault(r => r.Id == recipeId);

            var recipeViewModel = new SingleRecipeViewModel()
            {
                Id = searchedRecipe.Id,
                Name = searchedRecipe.Name,
                Category = CapitalizeOnlyFirstLetterOfCategoryName(searchedRecipe.Category),
                ImagePath = searchedRecipe.ImageId + searchedRecipe.Image.Extension,
                Ingredients = DetermineSingleIngredient(searchedRecipe.Ingridients),
                SubIngredients = DetermineSubIngredients(searchedRecipe.SubIngredients),
                Instructions = searchedRecipe.Instructions,
                CookingTime = searchedRecipe.CookingTime,
                Servings = searchedRecipe.Servings
            };

            return recipeViewModel;
        }

        public ChangeRecipeDisplayInfo RetrieveInfoForChangeRecipe(string recipeId) 
        {
            var searchedRecipe = db.Recipies.FirstOrDefault(r => r.Id == recipeId);

            var recipeForChange = new ChangeRecipeDisplayInfo()
            {
                Id = searchedRecipe.Id,
                Name = searchedRecipe.Name,
                CookingTime = searchedRecipe.CookingTime,
                Category = CapitalizeOnlyFirstLetterOfCategoryName(searchedRecipe.Category),
                AllIngredients = searchedRecipe.Ingridients,
                Instructions = searchedRecipe.Instructions,
                Servings = searchedRecipe.Servings
            };

            if (searchedRecipe.SubIngredients != null)
            {
                recipeForChange.AllIngredients += "\n\n" + searchedRecipe.SubIngredients;
            }

            return recipeForChange;
        }

        public async Task ChangeRecipe(string recipeId, ChangeRecipeFormModel model) 
        {
            var currentRecipe = db.Recipies.FirstOrDefault(r => r.Id == recipeId);

            var ingredientsArray = SplitIngredients(model.AllIngredients);

            currentRecipe.Name = model.Name;
            currentRecipe.Category = CapitalizeOnlyFirstLetterOfCategoryName(model.Category);
            currentRecipe.CookingTime = model.CookingTime;
            currentRecipe.Servings = model.Servings;
            currentRecipe.Instructions = model.Instructions;
            currentRecipe.Ingridients = ingredientsArray[0];

            if (ingredientsArray.Count() > 1)
            {
                currentRecipe.SubIngredients = ingredientsArray[1];
            }

            if (model.Image != null)
            {
                var imageToDelete = db.Images.FirstOrDefault(i => i.RecipeId == recipeId);

                db.Images.Remove(imageToDelete);

                string extension = Path.GetExtension(model.Image.FileName);

                Image image = new Image()
                {
                    Name = Guid.NewGuid().ToString(),
                    Extension = extension
                };

                currentRecipe.Image = image;
                currentRecipe.ImageId = image.Id;

                SavePicture(model.Image, image.Id);
            }

            await db.SaveChangesAsync();
        }

        public async Task DeleteRecipe(string recipeId) 
        {
            var currentRecipe = db.Recipies.FirstOrDefault(r => r.Id == recipeId);

            currentRecipe.IsDeleted = true;

            await db.SaveChangesAsync();
        }

        public IEnumerable<SingleRecipeViewModel> GetRecipesByCategory(string catergory) 
        {
            var recipes = db.Recipies.Where(r => r.Category.ToLower() == catergory.ToLower()).OrderByDescending(r => r.CreatedOn).Select(r => new SingleRecipeViewModel() 
            {
                Id = r.Id,
                Name = r.Name,
                Category = r.Category.ToUpper(),
                Servings = r.Servings,
                CookingTime = r.CookingTime,
                ImagePath = r.ImageId + r.Image.Extension,
            }
            ).ToList();

            return recipes;
        }

        private string[] SplitIngredients(string allIngredients) 
        {
            var arr = allIngredients.Split("\r\n\r\n",2);

            return arr;
        }

        private List<string> DetermineSingleIngredient(string ingredients) 
        {
            var ingredientsList = ingredients.Split("\n").ToList();

            return ingredientsList;
        }

        private List<SubIngredientViewModel> DetermineSubIngredients(string subIngredients)
        {
            if (subIngredients is null)
            {
                return null;
            }

            List<SubIngredientViewModel> list = new List<SubIngredientViewModel>();

            var subIngredientsList = subIngredients.Split("\r\n\r\n").ToList();

            foreach (var subIngredient in subIngredientsList)
            {
                SubIngredientViewModel subIngredientModel = new SubIngredientViewModel();

                var ingredientsList = subIngredient.Split("\n").ToList();

                subIngredientModel.Name = ingredientsList[0];

                subIngredientModel.Ingredients = DetermineSingleIngredient(subIngredient).Skip(1).ToList();

                list.Add(subIngredientModel);
            }

            return list;
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

        private string CapitalizeOnlyFirstLetterOfCategoryName(string category) 
        {
            var result = char.ToUpper(category[0]) + category.Substring(1);

            return result;
        }

        
    }
}
