using System;
using System.Collections.Generic;
using System.Text;

namespace UserManaging.CQRS.Queries
{
    public class QueryUserResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public decimal Expenses { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserAccountDto Account { get; set; }
    }

    public class UserAccountDto
    {
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
    }
}
