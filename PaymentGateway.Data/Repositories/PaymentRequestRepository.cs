using System;
using System.Threading.Tasks;

using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Data.Repositories
{
  /// <inheritdoc cref="IPaymentRequestRepository"/>
  public class PaymentRequestRepository : IPaymentRequestRepository
  {
    private readonly SQLiteDbContext _context;

    public PaymentRequestRepository(SQLiteDbContext db)
    {
      _context = db;
    }

    public async Task AddAsync(PaymentRequest paymentRequest)
    {
      await _context.AddAsync(paymentRequest);
      await _context.SaveChangesAsync();
    }

    public async Task<PaymentRequest> FindAsync(Guid id)
    {
      return await _context.PaymentRequests.FindAsync(id);
    }
  }
}
