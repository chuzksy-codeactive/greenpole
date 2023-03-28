using Domain.Common;

namespace Domain.Entities
{
    public class PaymentRecord : AuditableEntity
    {
        public Guid Id { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Narration { get; set; }
    }
}
