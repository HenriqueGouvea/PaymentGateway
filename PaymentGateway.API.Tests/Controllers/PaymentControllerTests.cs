﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PaymentGateway.API.Controllers;
using PaymentGateway.API.DTO;
using PaymentGateway.Services.PaymentProcessing;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.API.Tests.Controllers
{
  public class PaymentControllerTests
  {
    private PaymentController _target;
    private Mock<IPaymentProcessingService> _paymentProcessingService;

    [SetUp]
    public void Setup()
    {
      _paymentProcessingService = new Mock<IPaymentProcessingService>();
      _target = new PaymentController(_paymentProcessingService.Object);
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
      var paymentRequest = new PaymentRequest
      {
        Name = "Bill Gates",
        Amount = 1,
        Currency = "EUR",
        CVV = 123,
        ExpiryMonth = 12,
        ExpiryYear = 2021,
        Number = "4657789945661234"
      };

      var id = Guid.NewGuid();
      var serviceResponse = new PaymentProcessingResponse(id, "Approved");
      _paymentProcessingService.Setup(s => s.Process(It.IsAny<PaymentProcessingRequest>())).ReturnsAsync(serviceResponse);

      // Act
      var result = await _target.ProcessPayment(paymentRequest) as CreatedResult;

      // Assert
      Assert.IsNotNull(result);
      Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
      Assert.AreEqual($"https://localhost:44365/api/payment/{id}", result.Location);
      // Assert if it's called the repository to save the payment processing.
    }
  }
}