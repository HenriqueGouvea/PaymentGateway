using System;
using System.Threading.Tasks;

using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Domain.Services
{
  /// <inheritdoc cref="IPaymentRequestService"/>>
  public class PaymentRequestService : IPaymentRequestService
  {
    private readonly IPaymentRequestRepository _repository;

    public PaymentRequestService(IPaymentRequestRepository repository)
    {
      _repository = repository;
    }

    public async Task AddAsync(PaymentRequest paymentRequest)
    {
      await _repository.AddAsync(paymentRequest);
    }

    public async Task<PaymentRequest> FindAsync(Guid id)
    {
      return await _repository.FindAsync(id);
    }
  }
}
