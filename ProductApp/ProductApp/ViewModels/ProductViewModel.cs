using System.ComponentModel.DataAnnotations;

namespace ProductApp.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 1 and 100 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, MinimumLength = 2, ErrorMessage = "Description must be between 1 and 1000 characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a natural number")]
        public int Quantity { get; set; }
    }
}
