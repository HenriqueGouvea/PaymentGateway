using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.API.DTO;
using PaymentGateway.Domain.Services;
using System;
using System.Threading.Tasks;

namespace PaymentGateway.API.Controllers
{
  /// <summary>
  /// Controller responsible for the user account.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : Controller
  {
    private readonly IUserService _userService;
    private readonly ILogger<AccountController> _log;

    public AccountController(IUserService userService, ILogger<AccountController> log)
    {
      _userService = userService;
      _log = log;
    }

    [Route("login")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate(UserRequest userRequest)
    {
      try
      {
        var user = await _userService.GetUserAsync(userRequest.UserName, userRequest.Password);

        if (user == null)
        {
          return NotFound(new { Message = "It was not found a user with this name and password" });
        }

        var token = _userService.GenerateToken(user);

        return Ok(new UserResponse(user.UserName, token));
      }
      catch (Exception e)
      {
        _log.LogError("Error during authentication", e);

        throw;
      }
    }
  }
}