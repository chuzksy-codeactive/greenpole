using Application.Contracts;
using Application.DTOs;
using Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentGatewayService _paymentService;
        public PaymentsController(IPaymentGatewayService paymentService)
        {
            _paymentService = paymentService;
        }

        
        [HttpPost("update-event")]
        [ProducesResponseType(typeof(SuccessResponse<PaymentUpdateEventResponseDto>), 200)]
        public async Task<IActionResult> PaymentUpdateEvent(PaymentUpdateEventInputDto model)
        {
            var response = await _paymentService.PaymentUpdateEvent(model);
            return Ok(response);
        }

        [HttpPost("dividend-payout")]
        [ProducesResponseType(typeof(SuccessResponse<DividendPayoutResponseDto>), 200)]
        public async Task<IActionResult> DividendPayout(DividendPayoutInputDto model)
        {
            var response = await _paymentService.DividendPayout(model);
            return Ok(response);
        }

        [HttpPost("dividend-record")]
        [ProducesResponseType(typeof(SuccessResponse<DividendRecordResponseDto>), 200)]
        public async Task<IActionResult> CreateDividendRecord(DividendRecordInputDto model)
        {
            var response = await _paymentService.CreateDividendRecord(model);
            return Ok(response);
        }
    }
}
