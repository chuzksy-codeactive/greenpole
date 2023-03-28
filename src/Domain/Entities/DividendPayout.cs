using Domain.Enums;

namespace Domain.Entities
{
    public class DividendPayout
    {
        public Guid Id { get; set; }
        public string PayoutTitle { get; set; }
        public EDividendStatus Status { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string PaymentHistory { get; set; }
        public ICollection<DividendRecord> DividendRecords { get; set; }
    }
}
