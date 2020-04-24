using System;

namespace PaymentGateway.Services.PaymentProcessing
{
  /// <summary>
  /// The processing payment response.
  /// </summary>
  public class PaymentProcessingResponse
  {
    public PaymentProcessingResponse(Guid id, string status)
    {
      Id = id;
      Status = status;
    }

    public Guid Id { get; }

    public string Status { get; }
  }
}
