using System;

namespace PaymentGateway.Services.PaymentProcessing
{
  /// <summary>
  /// The processing payment response.
  /// </summary>
  public class PaymentProcessingResponse
  {
    public PaymentProcessingResponse(Guid id, bool isSuccessful)
    {
      Id = id;
      IsSuccessful = isSuccessful;
    }

    public Guid Id { get; }

    public bool IsSuccessful { get; }
  }
}
