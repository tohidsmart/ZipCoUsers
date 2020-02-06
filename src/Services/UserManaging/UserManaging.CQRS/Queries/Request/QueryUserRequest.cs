using System;
using MediatR;

namespace UserManaging.CQRS.Queries
{
    public class QueryUserRequest : IRequest<QueryUserResponse>
    {
        public Guid UserId { get; set; }
    }
}
