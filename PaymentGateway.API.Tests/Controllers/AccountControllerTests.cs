using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PaymentGateway.API.Controllers;
using PaymentGateway.API.DTO;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Services;
using System.Threading.Tasks;

namespace PaymentGateway.API.Tests.Controllers
{
  public class AccountControllerTests
  {
    private AccountController _target;
    private Mock<IUserService> _userService;
    private Mock<ILogger<AccountController>> _log;

    [SetUp]
    public void Setup()
    {
      _userService = new Mock<IUserService>();
      _log = new Mock<ILogger<AccountController>>();
      _target = new AccountController(_userService.Object, _log.Object);
    }

    [Test]
    public async Task Authenticate_UserNotFound_ReturnsNotFound()
    {
      // Arrange
      var userRequest = new UserRequest
                        {
                          UserName = "user.name",
                          Password = "123456",
                        };

      // Act
      var result = await _target.Authenticate(userRequest) as NotFoundObjectResult;

      Assert.IsNotNull(result);
      Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
      _userService.Verify(s => s.GetUserAsync(userRequest.UserName, userRequest.Password), Times.Once);
    }

    [Test]
    public async Task Authenticate_UserFound_TokenGenerated()
    {
      // Arrange
      var userRequest = new UserRequest
                        {
                          UserName = "user.name",
                          Password = "123456",
                        };

      var user = new User();
      const string Token = "token123456";

      _userService.Setup(s => s.GetUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);
      _userService.Setup(s => s.GenerateToken(user)).Returns(Token);

      // Act
      var result = await _target.Authenticate(userRequest) as OkObjectResult;

      Assert.IsNotNull(result);
      _userService.Verify(s => s.GenerateToken(user), Times.Once);
      
      var responseObject = result.Value as UserResponse;

      Assert.IsNotNull(responseObject);
      Assert.AreEqual(Token, responseObject.Token);
    }

    [Test]
    public async Task Authenticate_UserFound_ReturnsOk()
    {
      // Arrange
      var userRequest = new UserRequest
                        {
                          UserName = "user.name",
                          Password = "123456",
                        };

      var user = new User { UserName = "user.name" };

      _userService.Setup(s => s.GetUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);

      // Act
      var result = await _target.Authenticate(userRequest) as OkObjectResult;

      Assert.IsNotNull(result);
      Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
      
      var responseObject = result.Value as UserResponse;

      Assert.IsNotNull(responseObject);
      Assert.AreEqual(userRequest.UserName, responseObject.UserName);
    }
  }
}
