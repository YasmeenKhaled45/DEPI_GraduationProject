using GraduationProject.Models;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Service> Services { get; set; }

    }
}
