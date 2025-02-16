namespace GraduationProject.Services
{
    public interface IPaymentService
    {
        Task<string?> CreateOrUpdatePaymentIntent(string serviceId, decimal price);
        Task<bool> UpdatePaymentStatus(string paymentIntentId, bool isSuccess);
       // Task<bool> HandleWebhook(string payload, string sigHeader);
    }
}
