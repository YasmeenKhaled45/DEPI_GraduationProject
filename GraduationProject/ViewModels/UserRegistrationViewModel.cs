using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModels
{
    public class UserRegistrationViewModel
    {
        // User properties
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string PhoneNumber { get; set; }

        public string? ProfilePic { get; set; } // Optional

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        // Collection of Addresses
        public List<AddressViewModel> Addresses { get; set; } = new List<AddressViewModel>();

        // Additional fields (e.g., for Identity User)
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class AddressViewModel
    {
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Postal Code format")]
        public string PostalCode { get; set; }
    }
}
