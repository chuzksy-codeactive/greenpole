using Application.Contracts;
using Application.DTOs;
using Application.Helpers;
using Domain.Entities;
using Infrastructure.Data.AppDbContext;
using MapsterMapper;

namespace Application.Services
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PaymentGatewayService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SuccessResponse<DividendRecordResponseDto>> CreateDividendRecord(DividendRecordInputDto input)
        {
            var dividendRecord = _mapper.Map<DividendRecord>(input);
            await _context.DividendRecords.AddAsync(dividendRecord);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<DividendRecordResponseDto>(dividendRecord);

            return new SuccessResponse<DividendRecordResponseDto>
            {
                Message = "Dividend record created successfully",
                Data = result
            };
        }

        public async Task<SuccessResponse<DividendPayoutResponseDto>> DividendPayout(DividendPayoutInputDto input)
        {
            var dividendPayout = _mapper.Map<DividendPayout>(input);
            await _context.DividendPayouts.AddAsync(dividendPayout);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<DividendPayoutResponseDto>(dividendPayout);

            // _scheduler.ScheduleJob(new DividendPayoutJob(payout.DividendPayoutId));

            // TODO: send email on new payout created

            return new SuccessResponse<DividendPayoutResponseDto>
            {
                Message = "Dividend payout created successfully",
                Data = result
            };
        }

        public async Task<SuccessResponse<PaymentUpdateEventResponseDto>> PaymentUpdateEvent(PaymentUpdateEventInputDto input)
        {
            return new SuccessResponse<PaymentUpdateEventResponseDto>
            {
                Message = "Payment event updated successfully",
                Data = null
            };
        }
    }
}
