using Microsoft.Extensions.Configuration;
using Stripe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraduationProject.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;

        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Create or Update Payment Intent
        public async Task<string?> CreateOrUpdatePaymentIntent(string serviceId, decimal price)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var paymentIntentService = new PaymentIntentService();
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(price * 100), // Convert price to cents
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
                Metadata = new Dictionary<string, string>
                {
                    { "ServiceId", serviceId }
                }
            };

            var paymentIntent = await paymentIntentService.CreateAsync(options);
            return paymentIntent.ClientSecret;
        }

        // Update payment status based on success or failure
        public async Task<bool> UpdatePaymentStatus(string paymentIntentId, bool isSuccess)
        {
            return await Task.FromResult(isSuccess);
        }

        //// Handle Stripe Webhook
        //public async Task<bool> HandleWebhook(string payload, string sigHeader)
        //{
        //    var webhookSecret = _configuration["StripeSettings:WebhookSecret"];
        //    var eventObj = EventUtility.ConstructEvent(payload, sigHeader, webhookSecret);

        //    switch (eventObj.Type)
        //    {
        //        case Events.PaymentIntentSucceeded:
        //            var paymentIntentSucceeded = eventObj.Data.Object as PaymentIntent;
        //            await UpdatePaymentStatus(paymentIntentSucceeded.Id, true);
        //            break;

        //        case Events.PaymentIntentFailed:
        //            var paymentIntentFailed = eventObj.Data.Object as PaymentIntent;
        //            await UpdatePaymentStatus(paymentIntentFailed.Id, false);
        //            break;

        //        default:
        //            return false;
        //    }

        //    return true;
        //}
    }
}
