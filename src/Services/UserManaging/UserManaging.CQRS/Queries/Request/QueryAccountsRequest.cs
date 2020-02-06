using System.Collections.Generic;
using MediatR;

namespace UserManaging.CQRS.Queries
{
    public class QueryAccountsRequest : IRequest<IEnumerable<QueryAccountResponse>>
    {
    }
}
