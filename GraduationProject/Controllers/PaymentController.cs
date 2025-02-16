using GraduationProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Threading.Tasks;

namespace GraduationProject.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // Show payment page
        [HttpGet]
        public IActionResult Pay(int serviceId, decimal price)
        {
            // Pass the service details to the view if needed
            ViewBag.ServiceId = serviceId;
            ViewBag.Price = price;
            return View();
        }

        // Action to create payment and return the client secret
        [HttpPost]
        public async Task<IActionResult> CreatePayment(int serviceId, decimal price)
        {
            var clientSecret = await _paymentService.CreateOrUpdatePaymentIntent(serviceId.ToString(), price);

            if (clientSecret != null)
            {
                // Return the client secret to the frontend for completing the payment
                return Json(new { clientSecret });
            }

            return BadRequest("Failed to create payment intent.");
        }

        // Action to handle payment status update (e.g., from webhook)
        [HttpPost]
        public async Task<IActionResult> UpdatePaymentStatus(string paymentIntentId, bool isSuccess)
        {
            var result = await _paymentService.UpdatePaymentStatus(paymentIntentId, isSuccess);

            if (result)
            {
                return Ok("Payment status updated.");
            }

            return BadRequest("Failed to update payment status.");
        }

        // Handle Stripe Webhook (called by Stripe to notify of payment success or failure)
        //[HttpPost]
        //public async Task<IActionResult> Webhook([FromBody] string payload, [FromHeader(Name = "Stripe-Signature")] string sigHeader)
        //{
        //    var isSuccess = await _paymentService.HandleWebhook(payload, sigHeader);

        //    if (isSuccess)
        //    {
        //        return Ok("Webhook processed successfully.");
        //    }

        //    return BadRequest("Failed to process webhook.");
        //}
    }
}
