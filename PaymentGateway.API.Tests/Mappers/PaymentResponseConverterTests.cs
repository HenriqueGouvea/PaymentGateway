using System;
using NUnit.Framework;
using PaymentGateway.API.DTO;
using PaymentGateway.API.Mappers;
using PaymentGateway.Services.PaymentProcessing;

namespace PaymentGateway.API.Tests.Mappers
{
  public class PaymentResponseConverterTests
  {
    [Test]
    public void PaymentResponseConverter_ToServicePaymentRequest_FieldsAreEqual()
    {
      // Arrange
      var paymentProcessingResponse = new PaymentProcessingResponse(Guid.NewGuid(), "Approved");

      var paymentRequest = new PaymentRequest
      {
        Number = "1111111111111111",
        Name = "Test",
        ExpiryYear = 2021,
        ExpiryMonth = 12,
        Currency = "EUR",
        CVV = 123,
        Amount = 20
      };

      // Act
      var paymentResponse = paymentProcessingResponse.ToPaymentResponse(paymentRequest);

      // Assert
      Assert.IsNotNull(paymentResponse);
      Assert.IsTrue(AreFieldsEqual(paymentResponse, paymentProcessingResponse, paymentRequest));
    }

    private static bool AreFieldsEqual(PaymentResponse paymentResponse, PaymentProcessingResponse paymentProcessingResponse, PaymentRequest paymentRequest)
    {
      return paymentResponse.Name == paymentRequest.Name &&
             paymentResponse.Amount == paymentRequest.Amount &&
             paymentResponse.Currency == paymentRequest.Currency &&
             paymentResponse.ExpiryMonth == paymentRequest.ExpiryMonth &&
             paymentResponse.ExpiryYear == paymentRequest.ExpiryYear &&
             paymentResponse.LastCardNumbers == paymentRequest.Number.Substring(paymentRequest.Number.Length - 4) &&
             paymentResponse.Status == paymentProcessingResponse.Status &&
             paymentResponse.Id == paymentProcessingResponse.Id;

    }
  }
}
