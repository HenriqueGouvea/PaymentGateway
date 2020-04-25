using NUnit.Framework;
using PaymentGateway.API.DTO;
using PaymentGateway.API.Mappers;
using PaymentGateway.Services.PaymentProcessing;

namespace PaymentGateway.API.Tests.Mappers
{
  public class PaymentRequestConverterTests
  {
    [Test]
    public void PaymentRequestConverter_ToServicePaymentRequest_FieldsAreEqual()
    {
      // Arrange
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
      var servicePaymentRequest = paymentRequest.ToServicePaymentRequest();

      // Assert
      Assert.IsNotNull(servicePaymentRequest);
      Assert.IsTrue(AreFieldsEqual(paymentRequest, servicePaymentRequest));
    }

    private static bool AreFieldsEqual(PaymentRequest paymentRequest, PaymentProcessingRequest servicePaymentRequest)
    {
      return paymentRequest.Number == servicePaymentRequest.Number &&
             paymentRequest.Amount == servicePaymentRequest.Amount &&
             paymentRequest.CVV == servicePaymentRequest.CVV &&
             paymentRequest.Currency == servicePaymentRequest.Currency &&
             paymentRequest.ExpiryMonth == servicePaymentRequest.ExpiryMonth &&
             paymentRequest.ExpiryYear == servicePaymentRequest.ExpiryYear &&
             paymentRequest.Name == servicePaymentRequest.Name;
    }
  }
}
