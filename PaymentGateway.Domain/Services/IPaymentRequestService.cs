using System;
using System.Threading.Tasks;

using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Services
{
  /// <summary>
  /// The PaymentRequestService is responsible for handle and read data from the repository related to the payment requests.
  /// </summary>
  public interface IPaymentRequestService
  {
    Task AddAsync(PaymentRequest paymentRequest);

    Task<PaymentRequest> FindAsync(Guid id);
  }
}
