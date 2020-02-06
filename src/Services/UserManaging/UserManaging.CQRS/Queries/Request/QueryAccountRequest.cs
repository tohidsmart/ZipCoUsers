using System;
using MediatR;

namespace UserManaging.CQRS.Queries
{
    public class QueryAccountRequest : IRequest<QueryAccountResponse>
    {
        public Guid AccountId { get; set; }
    }

}
