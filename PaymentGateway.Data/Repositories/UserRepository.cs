using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Repositories;
using System.Threading.Tasks;

namespace PaymentGateway.Data.Repositories
{
  /// <inheritdoc cref="IUserRepository"/>
  public class UserRepository : IUserRepository
  {
    private readonly SQLiteDbContext _context;

    public UserRepository(SQLiteDbContext db)
    {
      _context = db;
    }

    public async Task<User> GetUserAsync(string userName, string password)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
    }
  }
}
