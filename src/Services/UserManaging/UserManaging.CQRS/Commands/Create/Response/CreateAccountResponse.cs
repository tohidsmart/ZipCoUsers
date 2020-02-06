using System;
using System.Collections.Generic;
using UserManaging.Domain;

namespace UserManaging.CQRS.Commands.Create
{
    public class CreateAccountResponse
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Link> Links { get; set; } = new List<Link>
        {
            new Link
            {
                Href="/list",
                Rel="Accounts",
                Verb="GET"
            },
            new Link
            {
                Href="{id}",
                Rel="Account",
                Verb="GET"
            }
        };
    }
}
