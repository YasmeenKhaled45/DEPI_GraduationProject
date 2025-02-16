namespace GraduationProject.Data
{
    public class PaymentDetails
    {
        public int Id { get; set; } // Primary key
        public string ServiceId { get; set; } // The service being paid for
        public string PaymentIntentId { get; set; } // The ID of the PaymentIntent
        public decimal Amount { get; set; } // Amount for the service
        public string Status { get; set; } // Payment status (e.g., "Pending", "Paid", "Failed")
    }
}
