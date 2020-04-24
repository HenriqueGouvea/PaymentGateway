using System;

namespace PaymentGateway.Services.PaymentProcessing
{
  /// <summary>
  /// The processing payment request.
  /// </summary>
  public class PaymentProcessingRequest
  {
    Guid Id { get; }

    string Name { get; }

    string Number { get; }

    int ExpiryMonth { get; }

    int ExpiryYear { get; }

    decimal Amount { get; }

    string Currency { get; }

    int CVV { get; }
  }
}