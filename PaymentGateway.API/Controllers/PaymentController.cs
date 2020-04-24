using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.DTO;
using PaymentGateway.Services.PaymentProcessing;
using System.Threading.Tasks;
using PaymentGateway.API.Mappers;

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

      // Saved in the database

      // Save the process request in a log system

      return Created($"https://localhost:44365/api/payment/{paymentResponse.Id}", paymentResponse);
    }
  }
}