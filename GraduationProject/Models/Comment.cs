using GraduationProject.Models;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Comment is required")]
        public string Content { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }
        [Required]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
