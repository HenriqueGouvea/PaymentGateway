﻿using PaymentGateway.API.DTO;
using PaymentGateway.Services.PaymentProcessing;

namespace PaymentGateway.API.Mappers
{
  public static class PaymentResponseConverter
  {
    public static PaymentResponse ToPaymentResponse(this PaymentProcessingResponse serviceResponse, PaymentRequest apiRequest)
    {
      return new PaymentResponse
      {
        Id = serviceResponse.Id,
        Status = serviceResponse.Status,
        Name = apiRequest.Name,
        LastCardNumbers = GetLastCreditCardNumbers(apiRequest.Number),
        Amount = apiRequest.Amount,
        ExpiryMonth = apiRequest.ExpiryMonth,
        ExpiryYear = apiRequest.ExpiryYear,
        Currency = apiRequest.Currency
      };
    }

    public static Domain.Models.PaymentRequest ToDomainServicePaymentRequest(this PaymentProcessingResponse serviceResponse, PaymentRequest paymentRequest)
    {
      return new Domain.Models.PaymentRequest(
        serviceResponse.Id,
        paymentRequest.Name,
        GetLastCreditCardNumbers(paymentRequest.Number),
        paymentRequest.ExpiryMonth,
        paymentRequest.ExpiryYear,
        paymentRequest.Amount,
        paymentRequest.Currency,
        paymentRequest.CVV,
        serviceResponse.Status);
    }

    private static string GetLastCreditCardNumbers(string number)
    {
      var lastIndex = number.Length - 1;

      return number.Substring(lastIndex - 3);
    }
  }
}
