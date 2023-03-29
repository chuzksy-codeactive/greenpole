using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs
{
    public record PaymentUpdateEventResponseDto
    {
        public Guid Id { get; set; }
        public Guid PaymentScheduleId { get; set; }
        public Guid? PaymentRecordId { get; set; }
        public string TransactionStatus { get; set; }
        public string ResponseMessage { get; set; }
        public string UpdateType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? CreatedById { get; set; }
    }

    public record PaymentUpdateEventInputDto
    {
        public Guid PaymentScheduleId { get; set; }
        public Guid? PaymentRecordId { get; set; }
        public EPaymentEventType UpdateType { get; set; }
    }

    public record DividendPayoutInputDto
    {
        public required string PayoutTitle { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string ActorId { get; set; }
        public required DateTime PayoutDate { get; set; }
    }

    public record DividendPayoutResponseDto
    {
        public Guid Id { get; set; }
        public string PayoutTitle { get; set; }
        public EDividendStatus Status { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string PaymentHistory { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? CreatedById { get; set; }
        public string ActorId { get; set; }
        public string Activity { get; set; }
    }

    public record DividendRecordResponseDto
    {
        public Guid DividendPayoutId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountBankCode { get; set; }
        public string PaymentAmount { get; set; }
        public string ShareCertificateId { get; set; }
    }

    public record DividendRecordInputDto
    {
        public Guid DividendPayoutId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountBankCode { get; set; }
        public string PaymentAmount { get; set; }
        public string ShareCertificateId { get; set; }
        public Guid? CreatedById { get; set; }
    }

    public class ScheduleUpdateViewModel
    {

    }
}
