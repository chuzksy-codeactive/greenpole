using Domain.Common;

namespace Domain.Entities
{
    public class DividendRecord : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid DividendPayoutId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountBankCode { get; set; }
        public string PaymentAmount { get; set; }
        public string ShareCertificateId { get; set; }
        public DividendPayout DividendPayout { get; set; }
    }
}
