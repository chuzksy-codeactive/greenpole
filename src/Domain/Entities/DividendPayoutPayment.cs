using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class DividendPayoutPayment : AuditableEntity
    {
        public Guid Id { get; set; }
        public EPaymentStatus PaymentGateway { get; set; }
        public EDebitStatus DebitStatus { get; set; }
        public string DebitResponseMessage { get; set; }
        public EPaymentStatus PaymentStatus { get; set; }
        public string SuccessfulCredits { get; set; }
        public string FailedCredits { get; set; }
        public Guid DividendPaymentId { get; set; }
    }
}
