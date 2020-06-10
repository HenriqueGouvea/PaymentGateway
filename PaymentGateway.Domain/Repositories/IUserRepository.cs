using System.Threading.Tasks;

using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Repositories
{
  /// <summary>
  /// The UserRepository is responsible for handle and read data from the database related to the users of the application.
  /// </summary>
  public interface IUserRepository
  {
    Task<User> GetUserAsync(string userName, string password);
  }
}
