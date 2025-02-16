using Azure.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GraduationProject.Models;

namespace GraduationProject.Models
{
    public class Service
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Request> Requests { get; set; }
    }

}
