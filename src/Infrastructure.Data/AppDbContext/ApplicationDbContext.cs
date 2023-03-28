using Domain.Entities;
using Domain.Entities.Identities;
using Infrastructure.Data.AppDbContext.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }

        public DbSet<DividendPayout> DividendPayouts { get; set; }
        public DbSet<DividendPayoutHistory> DividendPayoutHistories { get; set; }
        public DbSet<DividendPayoutPayment> DividendPayoutPayments { get; set; }
        public DbSet<DividendRecord> DividendRecords { get; set; }
        public DbSet<PaymentSchedule> PaymentSchedules { get; set; }
        public DbSet<PaymentUpdateEvent> PaymentUpdateEvents { get; set; }
        public DbSet<PaymentRecord> PaymentRecords { get; set; }
        public override DbSet<User> Users { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}
