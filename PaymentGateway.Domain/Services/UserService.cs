using Microsoft.IdentityModel.Tokens;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Repositories;
using PaymentGateway.Domain.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.Services
{
  /// <inheritdoc cref="IUserService"/>>
  public class UserService : IUserService
  {
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
      _repository = repository;
    }

    public async Task<User> GetUserAsync(string userName, string password)
    {
      return await _repository.GetUserAsync(userName, password);
    }

    public string GenerateToken(User user)
    {
      var key = Encoding.ASCII.GetBytes(Security.Secret);

      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(
          new[]
                {
                  new Claim(ClaimTypes.Name, user.UserName),
                  new Claim(ClaimTypes.Role, user.Role)
                }),
        Expires = DateTime.UtcNow.AddMinutes(Security.TokenExpirationMinutes),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
