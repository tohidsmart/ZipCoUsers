using System;
using System.Collections.Generic;
using UserManaging.Domain;


namespace UserManaging.CQRS.Commands.Create
{
    public class CreateUserResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public decimal Expenses { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Link> Links { get; set; } = new List<Link>
        {
            new Link
            {
                Href="api/v1/accounts",
                Rel="Account",
                Verb="POST"
            },
            new Link
            {
                Href="/list",
                Rel="Users",
                Verb="GET"
            },
            new Link
            {
                Href="{id}",
                Rel="User",
                Verb="GET"
            }
        };
    }

}
