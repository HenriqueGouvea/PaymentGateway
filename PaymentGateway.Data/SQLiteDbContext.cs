using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Models;

namespace PaymentGateway.Data
{
  public class SQLiteDbContext : DbContext
  {
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      options.UseSqlite("Data Source=PaymentGateway.db");
    }

    public DbSet<PaymentRequest> PaymentRequests { get; set; }
  }
}
