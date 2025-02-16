using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Rate
    {
        public int Id { get; set; } 

       public string UserId { get; set; }
        public User User { get; set; }


        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }    

        [Required]
        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5")]
        public int RateValue { get; set; }

        public string? Review { get; set; }

        //public int RequestId { get; set; }
        //public Request Request { get; set; }
    }

}
