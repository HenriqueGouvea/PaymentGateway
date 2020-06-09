using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.DTO;
using PaymentGateway.API.Mappers;
using PaymentGateway.Domain.Services;
using PaymentGateway.Services.PaymentProcessing;
using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

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
    private readonly IPaymentRequestService _paymentRequestService;
    private readonly ILogger<PaymentController> _log;

    public PaymentController(
      IPaymentProcessingService paymentProcessingService,
      IPaymentRequestService paymentRequestService,
      ILogger<PaymentController> log)
    {
      _paymentProcessingService = paymentProcessingService;
      _paymentRequestService = paymentRequestService;
      _log = log;
    }

    /// <summary>
    /// Process a payment request verifying if it's approved or not by the banking institution.
    /// </summary>
    /// <param name="request">The payment request.</param>
    /// <returns>The payment response and the link to recover the data about the payment processing.</returns>
    [HttpPost]
    public async Task<IActionResult> ProcessPayment(PaymentRequest request)
    {
      try
      {
        if (!ModelState.IsValid)
        {
          return BadRequest(ModelState);
        }

        var processPayment = request.ToServicePaymentRequest();
        var serviceResponse = await _paymentProcessingService.Process(processPayment);
        var paymentResponse = serviceResponse.ToPaymentResponse(request);
        var paymentRequest = serviceResponse.ToDomainServicePaymentRequest(request);

        _paymentRequestService.AddAsync(paymentRequest);

        _log.LogInformation("Payment processed", paymentRequest.Id);

        return Created($"https://localhost:44365/api/payment/{paymentResponse.Id}", paymentResponse);
      }
      catch (Exception e)
      {
        _log.LogError("Error during payment processing", e);

        throw;
      }
    }

    /// <summary>
    /// Gets the previously made payment request details.
    /// </summary>
    /// <param name="id">The identifier of the payment request.</param>
    /// <returns>The details of the payment request.</returns>
    [HttpGet]
    public async Task<IActionResult> GetPaymentRequest(Guid id)
    {
      try
      {
        var paymentRequest = await _paymentRequestService.FindAsync(id);

        if (paymentRequest == null)
        {
          return NotFound();
        }

        _log.LogInformation("Payment requested", paymentRequest.Id);

        return Ok(paymentRequest);
      }
      catch (Exception e)
      {
        _log.LogError("Error during payment requesting", e);

        throw;
      }
    }
  }
}