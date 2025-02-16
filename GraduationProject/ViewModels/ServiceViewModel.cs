using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string? Image { get; set; } 
        public IFormFile? ImageFile { get; set; } 

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
