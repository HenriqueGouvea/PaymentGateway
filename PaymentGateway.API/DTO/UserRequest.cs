using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.API.DTO
{
  /// <summary>
  /// The user request object.
  /// </summary>
  public class UserRequest
  {
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
