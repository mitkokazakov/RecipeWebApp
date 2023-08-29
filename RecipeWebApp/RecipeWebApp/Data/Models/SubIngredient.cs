using System.ComponentModel.DataAnnotations;

namespace RecipeWebApp.Data.Models
{
    public class SubIngredient
    {
        public SubIngredient()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Ingredients { get; set; } = null!;

        [Required]
        public string RecipeId { get; set; } = null!;

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
