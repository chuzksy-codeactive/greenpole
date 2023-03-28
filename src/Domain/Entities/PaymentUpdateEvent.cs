using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class PaymentUpdateEvent : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid PaymentScheduleId { get; set; }
        public Guid? PaymentRecordId { get; set; }
        public ETransactionStatus TransactionStatus { get; set; }
        public string ResponseMessage { get; set; }
        public EPaymentEventType UpdateType { get; set; }
    }
}
