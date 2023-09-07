using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeWebApp.DTO;

namespace RecipeWebApp.Services
{
    public interface IRecipeService
    {
        Task AddRecipe(AddRecipeFormSubmit model);

        IEnumerable<RecipeCoverViewModel> GetFirstSixRecipes();
    }
}
