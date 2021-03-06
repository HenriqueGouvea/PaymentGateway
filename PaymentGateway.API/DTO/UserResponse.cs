﻿namespace PaymentGateway.API.DTO
{
  /// <summary>
  /// The user response object.
  /// </summary>
  public class UserResponse
  {
    public UserResponse(string userName, string token)
    {
      UserName = userName;
      Token = token;
    }

    public string UserName { get; set; }
    
    public string Token { get; set; }
  }
}
