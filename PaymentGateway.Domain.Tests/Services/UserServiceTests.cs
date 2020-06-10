using Moq;
using NUnit.Framework;
using PaymentGateway.Domain.Repositories;
using PaymentGateway.Domain.Services;
using System.Threading.Tasks;

using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Tests.Services
{
  public class UserServiceTests
  {
    private UserService _target;
    private Mock<IUserRepository> _userRepository;

    [SetUp]
    public void Setup()
    {
      _userRepository = new Mock<IUserRepository>();

      _target = new UserService(_userRepository.Object);
    }

    [Test]
    public async Task GetUserAsync_ReceivesUserAndPassword_CallsRepository()
    {
      // Arrange
      const string User = "user.name";
      const string Password = "123456";

      // Act
      await _target.GetUserAsync(User, Password);

      // Assert
      _userRepository.Verify(r => r.GetUserAsync(User, Password), Times.Once);
    }

    [Test]
    public void GenerateToken_ReceivesUserAndPassword_CallsRepository()
    {
      // Arrange
      var user = new User
                 {
                   Id = 1,
                   UserName = "user.name",
                   Password = "123456",
                   Role = "admin"
                 };

      // Act
      var token = _target.GenerateToken(user);

      // Arrange
      Assert.IsNotEmpty(token);
    }
  }
}
