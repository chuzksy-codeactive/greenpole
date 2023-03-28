using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class PaymentSchedule : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public EPaymentGateway PaymentGateway { get; set; }
        public string DebitAccount { get; set; }
        public string DebitNarration { get; set; }
        public ICollection<PaymentRecord> PaymentRecords { get; set; }
    }
}
