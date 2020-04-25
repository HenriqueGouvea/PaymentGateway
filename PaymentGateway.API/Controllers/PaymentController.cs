using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.DTO;
using PaymentGateway.API.Mappers;
using PaymentGateway.Services.PaymentProcessing;
using System.Threading.Tasks;

namespace PaymentGateway.API.Controllers
{
  /// <summary>
  /// Controller responsible for the payment processing and reports.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class PaymentController : Controller
  {
    private readonly IPaymentProcessingService _paymentProcessingService;

    public PaymentController(IPaymentProcessingService paymentProcessingService)
    {
      _paymentProcessingService = paymentProcessingService;
    }

    /// <summary>
    /// Process a payment request verifying if it's approved or not by the banking institution.
    /// </summary>
    /// <param name="request">The payment request.</param>
    /// <returns>The payment response and the link to recover the data about the payment processing.</returns>
    [HttpPost]
    public async Task<IActionResult> ProcessPayment(PaymentRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var processPayment = request.ToServicePaymentRequest();
      var serviceResponse = await _paymentProcessingService.Process(processPayment);
      var paymentResponse = serviceResponse.ToPaymentResponse(request);

      // Saved in the database - update unit test

      // Save the process request in a log system - update unit test

      return Created($"https://localhost:44365/api/payment/{paymentResponse.Id}", paymentResponse);
    }
  }
}