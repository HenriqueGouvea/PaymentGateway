using PaymentGateway.API.DTO;
using PaymentGateway.Services.PaymentProcessing;

namespace PaymentGateway.API.Mappers
{
  public static class PaymentRequestConverter
  {
    public static PaymentProcessingRequest ToServicePaymentRequest(this PaymentRequest paymentRequest)
    {
      return new PaymentProcessingRequest(
        paymentRequest.Name, 
        paymentRequest.Number, 
        paymentRequest.ExpiryMonth, 
        paymentRequest.ExpiryYear, 
        paymentRequest.Amount, 
        paymentRequest.Currency, 
        paymentRequest.CVV);
    }
  }
}
