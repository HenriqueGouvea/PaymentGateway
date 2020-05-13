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

    public void Add(PaymentRequest paymentRequest)
    {
      _repository.Add(paymentRequest);
    }
  }
}
