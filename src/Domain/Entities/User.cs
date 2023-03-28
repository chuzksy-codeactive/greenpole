using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>, IAuditableEntity
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public bool Verified { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string Status { get; set; }
        public DateTime? LastLogin { get; set; }
        public ICollection<UserActivity> UserActivities { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? CreatedById { get; set; }
    }
}
