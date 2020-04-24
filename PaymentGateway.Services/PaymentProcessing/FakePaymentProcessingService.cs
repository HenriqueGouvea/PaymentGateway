using System;
using System.Threading.Tasks;

namespace PaymentGateway.Services.PaymentProcessing
{
  /// <summary>
  /// Fake PaymentProcessingService used for development purposes.
  /// </summary>
  public class FakePaymentProcessingService : IPaymentProcessingService
  {
    /// <summary>
    /// Processes the specified payment request.
    /// This fake implementation returns a result randomly.
    /// </summary>
    /// <param name="request">The payment request.</param>
    /// <returns>
    /// A fake bank response.
    /// </returns>
    public Task<PaymentProcessingResponse> Process(PaymentProcessingRequest request)
    {
      var randomResponse = new Random().Next(0, 1);

      return Task.FromResult(new PaymentProcessingResponse(Guid.NewGuid(), randomResponse == 1));
    }
  }
}
