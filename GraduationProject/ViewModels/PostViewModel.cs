using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Post Description is required")]
        [StringLength(500, ErrorMessage = "Content length must not exceed 500 characters")]
        public string Content { get; set; }
        public string? Image { get; set; } 
        public IFormFile? ImageFile { get; set; } 
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
