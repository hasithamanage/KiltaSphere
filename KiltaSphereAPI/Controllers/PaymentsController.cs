using KiltaSphereAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KiltaSphereAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/Payments/member/5
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetMemberPayments(int memberId)
        {
            var payments = await _paymentService.GetMemberPaymentsAsync(memberId);
            return Ok(payments);
        }

        // POST: api/Payments/5/pay
        [HttpPost("{paymentId}/pay")]
        public async Task<IActionResult> MarkAsPaid(int paymentId)
        {
            var success = await _paymentService.ProcessPaymentAsync(paymentId);
            if (!success) return NotFound("Maksua ei löytynyt.");

            return Ok(new { message = "Maksu merkitty suoritetuksi." });
        }
    }
}