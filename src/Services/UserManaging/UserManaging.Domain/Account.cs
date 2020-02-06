using System;

namespace UserManaging.Domain
{
    public class Account : BaseEntity
    {
        public Guid AccountId { get; set; } = Guid.NewGuid();

        public User User { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public AccountType Type { get; set; }
    }
}
