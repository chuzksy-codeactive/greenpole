using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class DividendPayoutHistory : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid DividendPayoutId { get; set; }
        public string ActorId { get; set; }
        public EPaymentActivitiyStatus Activity { get; set; }
    }
}
