using System.ComponentModel.DataAnnotations;
namespace GraduationProject.Models
{
    public class Request
    {
        public int Id { get; set; } 
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }

        public string UserId { get; set; } 
        public User User { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        public Payment Payment { get; set; }
    }
}
