using System;
using System.Threading.Tasks;

namespace PaymentGateway.Services.PaymentProcessing
{
  /// <inheritdoc cref="IPaymentProcessingService"/>
  public class PaymentProcessingService : IPaymentProcessingService
  {
    /// <inheritdoc cref="IPaymentProcessingService.Process(PaymentProcessingRequest)"/>
    public Task<PaymentProcessingResponse> Process(PaymentProcessingRequest request)
    {
      throw new NotImplementedException("This service is still not implemented.");
    }
  }
}
