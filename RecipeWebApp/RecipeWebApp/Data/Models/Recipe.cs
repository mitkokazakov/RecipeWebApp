using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebApp.Data.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Id = Guid.NewGuid().ToString();
            SubIngredients = new HashSet<SubIngredient>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Instructions { get; set; } = null!;

        [Required]
        public string Ingridients { get; set; } = null!;

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        public int CookingTime { get; set; }

        [Required]
        public int Servings { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public string ImageId { get; set; } = null!;
        public virtual Image Image { get; set; } = null!;

        public virtual HashSet<SubIngredient> SubIngredients { get; set; }
    }
}
