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

    public string Name { get; }

    public string Number { get; }

    public int ExpiryMonth { get; }

    public int ExpiryYear { get; }

    public decimal Amount { get; }

    public string Currency { get; }

    public int CVV { get; }
  }
}