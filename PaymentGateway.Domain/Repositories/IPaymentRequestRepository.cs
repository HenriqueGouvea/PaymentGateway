using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Repositories
{
  /// <summary>
  /// The PaymentRequestRepository is responsible for handle and read data from the database related to the payment requests.
  /// </summary>
  public interface IPaymentRequestRepository
  {
    void Add(PaymentRequest paymentRequest);
  }
}