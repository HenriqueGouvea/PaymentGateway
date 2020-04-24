using System;

namespace PaymentGateway.API.DTO
{
  /// <summary>
  /// The payment processing request object.
  /// </summary>
  public class PaymentResponse
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string LastCardNumbers { get; set; }

    public int ExpiryMonth { get; set; }

    public int ExpiryYear { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }

    public string Status { get; set; }
  }
}
