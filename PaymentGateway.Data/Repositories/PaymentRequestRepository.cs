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

    public void Add(PaymentRequest paymentRequest)
    {
      _context.Add(paymentRequest);
      _context.SaveChanges();
    }
  }
}
