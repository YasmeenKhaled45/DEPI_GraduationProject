using System;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
    public class Payment
    {
        public int Id { get; set; } 

        // You can associate the user who made the payment here
        // public string UserId { get; set; }
        // public User User { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public DateTime Date { get; set; } 

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [Required]
        public string PaymentIntentId { get; set; }

        public int RequestId { get; set; } 
        public Request Request { get; set; }

        [StringLength(50)]
        public string PaymentStatus { get; set; } 
    }
}
