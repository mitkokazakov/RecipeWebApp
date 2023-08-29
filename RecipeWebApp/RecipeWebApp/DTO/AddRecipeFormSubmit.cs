namespace RecipeWebApp.DTO
{
    public class AddRecipeFormSubmit
    {
        public string RecipeName { get; set; }

        public int CookingTime { get; set; }

        public int Servings { get; set; }

        public string Category { get; set; }

        public string AllIngredients { get; set; }

        public string Instructions { get; set; }

        public IFormFile Image { get; set; }
    }
}
