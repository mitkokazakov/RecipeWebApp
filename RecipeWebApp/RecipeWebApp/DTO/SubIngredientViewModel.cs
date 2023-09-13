namespace RecipeWebApp.DTO
{
    public class SubIngredientViewModel
    {
        public string Name { get; set; } = null!;

        public List<string> Ingredients { get; set; } = new List<string>();
    }
}
