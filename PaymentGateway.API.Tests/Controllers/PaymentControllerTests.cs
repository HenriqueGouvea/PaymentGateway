using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PaymentGateway.API.Controllers;
using PaymentGateway.API.DTO;
using PaymentGateway.Domain.Services;
using PaymentGateway.Services.PaymentProcessing;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.API.Tests.Controllers
{
  public class PaymentControllerTests
  {
    private PaymentController _target;
    private Mock<IPaymentProcessingService> _paymentProcessingService;
    private Mock<IPaymentRequestService> _paymentRequestService;
    private Mock<ILogger<PaymentController>> _log;

    [SetUp]
    public void Setup()
    {
      _paymentProcessingService = new Mock<IPaymentProcessingService>();
      _paymentRequestService = new Mock<IPaymentRequestService>();
      _log = new Mock<ILogger<PaymentController>>();
      _target = new PaymentController(_paymentProcessingService.Object, _paymentRequestService.Object, _log.Object);
    }

    [Test]
    public async Task ProcessPayment_ModelStateInvalid_ReturnsBadRequest()
    {
      // Arrange
      var paymentRequest = new PaymentRequest
      {
        Name = string.Empty,
        Amount = 1,
        Currency = "EUR",
        CVV = 123,
        ExpiryMonth = 12,
        ExpiryYear = 2021,
        Number = "4657789945661234"
      };

      _target.ModelState.AddModelError("Test", "Test error message");

      // Act
      var result = await _target.ProcessPayment(paymentRequest) as BadRequestObjectResult;

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
    }

    [Test]
    public async Task ProcessPayment_ModelStateValid_ReturnsOk()
    {
      // Arrange
      var paymentRequest = GetValidDummyPaymentRequest();

      var id = Guid.NewGuid();
      var serviceResponse = new PaymentProcessingResponse(id, "Approved");
      _paymentProcessingService.Setup(s => s.Process(It.IsAny<PaymentProcessingRequest>())).ReturnsAsync(serviceResponse);

      // Act
      var result = await _target.ProcessPayment(paymentRequest) as CreatedResult;

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
      Assert.AreEqual($"https://localhost:44365/api/payment/{id}", result.Location);
      _paymentRequestService.Verify(s => s.AddAsync(It.IsAny<Domain.Models.PaymentRequest>()), Times.Once);
    }

    [Test]
    public async Task GetPaymentRequest_PaymentRequestNotFound_ReturnsBadRequest()
    {
      // Arrange
      var id = Guid.NewGuid();

      // Act
      var result = await _target.GetPaymentRequest(id) as NotFoundObjectResult;

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
      _paymentRequestService.Verify(s => s.FindAsync(id), Times.Once);
    }

    [Test]
    public async Task GetPaymentRequest_PaymentRequestFound_ReturnsOk()
    {
      // Arrange
      var id = Guid.NewGuid();
      _paymentRequestService.Setup(s => s.FindAsync(id)).ReturnsAsync(new Domain.Models.PaymentRequest());

      // Act
      var result = await _target.GetPaymentRequest(id) as OkObjectResult;

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
      _paymentRequestService.Verify(s => s.FindAsync(id), Times.Once);
    }

    private PaymentRequest GetValidDummyPaymentRequest()
    {
      return new PaymentRequest
             {
               Name = "Bill Gates",
               Amount = 1,
               Currency = "EUR",
               CVV = 123,
               ExpiryMonth = 12,
               ExpiryYear = 2021,
               Number = "4657789945661234"
             };
    }
  }
}
