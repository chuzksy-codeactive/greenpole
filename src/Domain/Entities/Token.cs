namespace Domain.Entities
{
    public class Token
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid UserId { get; set; }
        public string TokenType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(3);
        public User User { get; set; }
    }
}
