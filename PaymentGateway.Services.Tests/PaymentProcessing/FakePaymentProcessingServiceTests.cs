using NUnit.Framework;
using PaymentGateway.Services.PaymentProcessing;
using System.Threading.Tasks;

namespace PaymentGateway.Services.Tests.PaymentProcessing
{
  public class FakePaymentProcessingServiceTests
  {
    private IPaymentProcessingService _target;

    [SetUp]
    public void Setup()
    {
      _target = new FakePaymentProcessingService();
    }

    [Test]
    public async Task Process_AnyPaymentRequest_ReturnsSomeStatus()
    {
      // Arrange
      var paymentRequest = new PaymentProcessingRequest("Test", "1111111111111111", 12, 2025, 20, "EUR", 132);

      // Act
      var result = await _target.Process(paymentRequest);

      // Assert
      Assert.IsNotNull(result);
      Assert.IsFalse(string.IsNullOrEmpty(result.Status));
    }
  }
}
