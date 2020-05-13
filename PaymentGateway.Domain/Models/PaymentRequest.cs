using System;

namespace PaymentGateway.Domain.Models
{
  /// <summary>
  /// The payment request that the customers will ask for the payment services.
  /// </summary>
  public class PaymentRequest
  {
    public PaymentRequest()
    {
    }

    public PaymentRequest(
      Guid id, 
      string name, 
      string lastCardNumbers, 
      int expiryMonth, 
      int expiryYear, 
      decimal amount, 
      string currency, 
      int cvv, 
      string status)
    {
      Id = id;
      Name = name;
      LastCardNumbers = lastCardNumbers;
      ExpiryMonth = expiryMonth;
      ExpiryYear = expiryYear;
      Amount = amount;
      Currency = currency;
      CVV = cvv;
      Status = status;
      CreationDate = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string LastCardNumbers { get; set; }

    public int ExpiryMonth { get; set; }

    public int ExpiryYear { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }

    public int CVV { get; set; }

    public string Status { get; set; }

    public DateTime CreationDate { get; set; }
  }
}
