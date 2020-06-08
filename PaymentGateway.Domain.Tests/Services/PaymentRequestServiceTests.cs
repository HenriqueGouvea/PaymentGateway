using Moq;
using NUnit.Framework;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Repositories;
using PaymentGateway.Domain.Services;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Tests.Services
{
  public class PaymentRequestServiceTests
  {
    private PaymentRequestService _target;
    private Mock<IPaymentRequestRepository> _paymentRequestRepository;

    [SetUp]
    public void Setup()
    {
      _paymentRequestRepository = new Mock<IPaymentRequestRepository>();

      _target = new PaymentRequestService(_paymentRequestRepository.Object);
    }

    [Test]
    public async Task Add_ValidPaymentRequest_CallsAddMethod()
    {
      // Arrange
      var paymentRequest = new PaymentRequest(
        Guid.NewGuid(),
        "name",
        "1234",
        2,
        2025,
        20,
        "EUR",
        123,
        "Approved");

      // Act
      await _target.AddAsync(paymentRequest);

      // Assert
      _paymentRequestRepository.Verify(r => r.AddAsync(It.IsAny<PaymentRequest>()), Times.Once);
    }

    [Test]
    public async Task Find_AValidIdIsPassed_FindsInTheRepositoryWithTheId()
    {
      // Arrange
      var id = Guid.NewGuid();

      // Act
      await _target.FindAsync(id);

      // Assert
      _paymentRequestRepository.Verify(r => r.FindAsync(id), Times.Once);
    }
  }
}