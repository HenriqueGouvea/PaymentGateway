using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.DTO;
using System.Threading.Tasks;

namespace PaymentGateway.API.Controllers
{
  /// <summary>
  /// Controller responsible for the payment processing and reports.
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class PaymentController : Controller
  {
    [HttpPost]
    public async Task<IActionResult> ProcessPayment(PaymentRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      // Map to a service object

      // Send asynchronously to the gateway layer

      // Receive an URI from gateway layer
      var uri = string.Empty;

      request.GenerateNewId();

      // Save the process request in a log system

      return Created(uri, request);
    }
  }
}