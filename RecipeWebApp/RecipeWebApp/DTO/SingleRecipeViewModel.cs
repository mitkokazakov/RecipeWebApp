namespace RecipeWebApp.DTO
{
    public class SingleRecipeViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string ImagePath { get; set; } = null!;

        public List<string> Ingredients { get; set; } = null!;

        public List<SubIngredientViewModel> SubIngredients { get; set; } = new List<SubIngredientViewModel>();
        public string Instructions { get; set; } = null!;
        public int CookingTime { get; set; }
        public int Servings { get; set; }
    }
}
