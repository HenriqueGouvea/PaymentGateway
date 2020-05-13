using Moq;
using NUnit.Framework;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Repositories;
using PaymentGateway.Domain.Services;
using System;

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
    public void Add_ValidPaymentRequest_CallsAddMethod()
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
      _target.Add(paymentRequest);

      // Assert
      _paymentRequestRepository.Verify(r => r.Add(It.IsAny<PaymentRequest>()), Times.Once);
    }
  }
}