namespace PaymentGateway.Domain.Settings
{
  /// <summary>
  /// Security settings.
  /// </summary>
  public static class Security
  {
    public static readonly string Secret = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92";
    public static readonly int TokenExpirationMinutes = 60;
  }
}
