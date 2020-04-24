using System.Threading.Tasks;

namespace PaymentGateway.Services.PaymentProcessing
{
  /// <summary>
  /// The payment processing service.
  /// This service is responsible for contact the bank service to check if the payment request is accepted or not.
  /// </summary>
  public interface IPaymentProcessingService
  {
    /// <summary>
    /// Processes the specified payment request.
    /// </summary>
    /// <param name="request">The payment request.</param>
    /// <returns>The response of the bank.</returns>
    Task<PaymentProcessingResponse> Process(PaymentProcessingRequest request);
  }
}
