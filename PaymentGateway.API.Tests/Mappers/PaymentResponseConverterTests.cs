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

    [Test]
    public void PaymentResponseConverter_ToDomainServicePaymentRequest_FieldsAreEqual()
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
      var paymentResponse = paymentProcessingResponse.ToDomainServicePaymentRequest(paymentRequest);

      // Assert
      Assert.IsNotNull(paymentResponse);
      Assert.IsTrue(AreFieldsEqual(paymentResponse, paymentProcessingResponse, paymentRequest));
    }

    private static bool AreFieldsEqual(PaymentResponse outputObject, PaymentProcessingResponse paymentProcessingResponse, PaymentRequest paymentRequest)
    {
      return outputObject.Name == paymentRequest.Name &&
             outputObject.Amount == paymentRequest.Amount &&
             outputObject.Currency == paymentRequest.Currency &&
             outputObject.ExpiryMonth == paymentRequest.ExpiryMonth &&
             outputObject.ExpiryYear == paymentRequest.ExpiryYear &&
             outputObject.LastCardNumbers == paymentRequest.Number.Substring(paymentRequest.Number.Length - 4) &&
             outputObject.Status == paymentProcessingResponse.Status &&
             outputObject.Id == paymentProcessingResponse.Id;

    }

    private static bool AreFieldsEqual(Domain.Models.PaymentRequest outputObject, PaymentProcessingResponse paymentProcessingResponse, PaymentRequest paymentRequest)
    {
      return outputObject.Id == paymentProcessingResponse.Id &&
             outputObject.Name == paymentRequest.Name &&
             outputObject.LastCardNumbers == paymentRequest.Number.Substring(paymentRequest.Number.Length - 4) &&
             outputObject.ExpiryMonth == paymentRequest.ExpiryMonth &&
             outputObject.ExpiryYear == paymentRequest.ExpiryYear &&
             outputObject.Amount == paymentRequest.Amount &&
             outputObject.Currency == paymentRequest.Currency &&
             outputObject.CVV == paymentRequest.CVV &&
             outputObject.Status == paymentProcessingResponse.Status;
    }
  }
}
