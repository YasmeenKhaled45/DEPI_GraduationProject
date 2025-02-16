using GraduationProject.Models;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Post Description is required")]
        public string Content { get; set; }
        public string? Image { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
