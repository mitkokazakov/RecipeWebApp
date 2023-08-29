using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWebApp.Data.Models
{
    public class Image
    {
        public Image()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Extension { get; set; } = null!;

        [Required]
        public string RecipeId { get; set; } = null!;

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
