using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static RecipeWebApp.DTO.FormModelsConstants;

namespace RecipeWebApp.DTO
{
    public class ChangeRecipeFormModel
    {
        [Required]
        [MinLength(RecipeNameMinLen, ErrorMessage = "{0} should be at least {1} characters long!")]
        [Display(Name = "Recipe Name")]
        public string Name { get; set; } = null!;

        [Required]
        [Range(CookingMinTime, CookingMaxTime)]
        [Display(Name = "Cooking Time")]
        public int CookingTime { get; set; }

        [Required]
        [Range(ServingsMinCount, ServingsMaxCount)]
        public int Servings { get; set; }

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        [MinLength(AllIngredinetsMinLen, ErrorMessage = "{0} should be at least {1} characters long!")]
        [Display(Name = "All Ingredients")]
        public string AllIngredients { get; set; } = null!;

        [Required]
        [MinLength(InstructionsMinLen, ErrorMessage = "{0} should be at least {1} characters long!")]
        public string Instructions { get; set; } = null!;

        public IFormFile? Image { get; set; } = null!;
    }
}
