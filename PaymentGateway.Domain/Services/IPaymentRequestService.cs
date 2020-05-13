using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Services
{
  /// <summary>
  /// The PaymentRequestService is responsible for handle and read data from the repository related to the payment requests.
  /// </summary>
  public interface IPaymentRequestService
  {
    void Add(PaymentRequest paymentRequest);
  }
}
