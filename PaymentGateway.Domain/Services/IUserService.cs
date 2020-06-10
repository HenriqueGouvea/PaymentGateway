using System.Threading.Tasks;

using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Services
{
  /// <summary>
  /// The UserService is responsible for handle and read data from the repository related to the users of the application.
  /// </summary>
  public interface IUserService
  {
    Task<User> GetUserAsync(string userName, string password);

    string GenerateToken(User user);
  }
}
