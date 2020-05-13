using System;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.DTO;
using PaymentGateway.API.Mappers;
using PaymentGateway.Services.PaymentProcessing;
using System.Threading.Tasks;
using PaymentGateway.Domain.Services;

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

    public PaymentController(
      IPaymentProcessingService paymentProcessingService,
      IPaymentRequestService paymentRequestService)
    {
      _paymentProcessingService = paymentProcessingService;
      _paymentRequestService = paymentRequestService;
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
      var paymentRequest = serviceResponse.ToDomainServicePaymentRequest(request);

      // Saved in the database - update unit tests (Repository, domain service and mapper)
      _paymentRequestService.Add(paymentRequest);

      // Save the process request in a log system - update unit test

      // Use CQRS

      // Change readme documentation

      // Add try-catches

      return Created($"https://localhost:44365/api/payment/{paymentResponse.Id}", paymentResponse);
    }

    /// <summary>
    /// Gets the previously made payment request details.
    /// </summary>
    /// <param name="id">The identifier of the payment request.</param>
    /// <returns>The details of the payment request.</returns>
    public async Task<IActionResult> GetPaymentRequest(Guid id)
    {
      // Implement this method
      return null;
    }
  }
}