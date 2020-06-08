using System;
using System.Threading.Tasks;

using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Repositories
{
  /// <summary>
  /// The PaymentRequestRepository is responsible for handle and read data from the database related to the payment requests.
  /// </summary>
  public interface IPaymentRequestRepository
  {
    Task AddAsync(PaymentRequest paymentRequest);

    Task<PaymentRequest> FindAsync(Guid id);
  }
}