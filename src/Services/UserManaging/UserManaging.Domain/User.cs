using System;

namespace UserManaging.Domain
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public decimal Salary { get; set; }

        public decimal Expenses { get; set; }

        public Account Account { get; set; }

    }
}
