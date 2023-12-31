﻿using System;
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

        SingleRecipeViewModel GetRecipeById(string recipeId);

        ChangeRecipeDisplayInfo RetrieveInfoForChangeRecipe(string recipeId);

        Task ChangeRecipe(string recipeId, ChangeRecipeFormModel model);

        Task DeleteRecipe(string id);

        IEnumerable<SingleRecipeViewModel> GetRecipesByCategory(string category);
    }
}
