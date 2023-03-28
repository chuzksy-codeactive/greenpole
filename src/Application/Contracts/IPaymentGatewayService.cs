using Application.DTOs;
using Application.Helpers;

namespace Application.Contracts
{
    public interface IPaymentGatewayService
    {
        Task<SuccessResponse<PaymentUpdateEventResponseDto>> PaymentUpdateEvent(PaymentUpdateEventInputDto input);
        Task<SuccessResponse<DividendPayoutResponseDto>> DividendPayout(DividendPayoutInputDto input);
        Task<SuccessResponse<DividendRecordResponseDto>> CreateDividendRecord(DividendRecordInputDto input);
    }
}
