using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using GraduationProject.Models;


public class User : IdentityUser
{
    [Required(ErrorMessage = "Phone number is required")]
    [Phone]
    public string PhoneNumber { get; set; }

    public string? ProfilePic { get; set; } // Optional

    public string Title { get; set; }
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Request> Requests { get; set; }

}

