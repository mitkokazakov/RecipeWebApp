using System.ComponentModel.DataAnnotations;

namespace RecipeWebApp.DTO
{
    public class AddRecipeFormSubmit
    {
        [Required]
        [MinLength(2,ErrorMessage ="Recipe Name should be at least 2 characters long!")]
        public string RecipeName { get; set; } = null!;

        [Required]
        [MinLength(5, ErrorMessage = "Cooking Time should last at least 5 minutes!")]
        public int CookingTime { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Servings should last at least 1!")]
        public int Servings { get; set; }

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        [MinLength(10, ErrorMessage = "Ingredients should be at least 10 characters long!")]
        public string AllIngredients { get; set; } = null!;

        [Required]
        [MinLength(10,ErrorMessage ="Instructions should be at least 10 characters long!")]
        public string Instructions { get; set; } = null!;

        [Required]
        public IFormFile Image { get; set; } = null!;
    }
}
