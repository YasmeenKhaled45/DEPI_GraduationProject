using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "address is required")]
        [StringLength(100, ErrorMessage = "address cannot exceed 100 characters")]
        public string address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Postal Code format")]
        public string PostalCode { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }

}
