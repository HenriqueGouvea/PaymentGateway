using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.API.DTO
{
  /// <summary>
  /// The payment processing request object.
  /// </summary>
  public class PaymentRequest
  {
    [Required]
    public string Name { get; set; }

    [Required]
    [CreditCard]
    public string Number { get; set; }

    [Required]
    [Range(1, 12)]
    public int ExpiryMonth { get; set; }

    [Required]
    [Range(2000, 2200)]
    public int ExpiryYear { get; set; }

    [Required]
    [Range(0, 999999999)]
    public decimal Amount { get; set; }

    [Required]
    public string Currency { get; set; }

    [Required]
    [Range(100, 9999)]
    public int CVV { get; set; }
  }
}
