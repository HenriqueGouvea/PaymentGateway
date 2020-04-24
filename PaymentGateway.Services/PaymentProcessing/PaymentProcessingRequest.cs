using System;

namespace PaymentGateway.Services.PaymentProcessing
{
  /// <summary>
  /// The processing payment request.
  /// </summary>
  public class PaymentProcessingRequest
  {
    public PaymentProcessingRequest(string name, string number, int expiryMonth, int expiryYear, decimal amount, string currency, int cvv)
    {
      Name = name;
      Number = number;
      ExpiryMonth = expiryMonth;
      ExpiryYear = expiryYear;
      Amount = amount;
      Currency = currency;
      CVV = cvv;
    }

    string Name { get; }

    string Number { get; }

    int ExpiryMonth { get; }

    int ExpiryYear { get; }

    decimal Amount { get; }

    string Currency { get; }

    int CVV { get; }
  }
}