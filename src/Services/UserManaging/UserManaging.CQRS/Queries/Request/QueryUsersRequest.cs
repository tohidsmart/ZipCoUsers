using System.Collections.Generic;
using MediatR;

namespace UserManaging.CQRS.Queries
{
    public class QueryUsersRequest : IRequest<IEnumerable<QueryUserResponse>>
    {
        
    }
}
