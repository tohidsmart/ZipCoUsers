using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UserManaging.CQRS.Queries
{
    class QueryAccountsHandler : IRequestHandler<QueryAccountsRequest, IEnumerable<QueryAccountResponse>>
    {
        private readonly IUserAccountQueries userAccountQueries;

        public QueryAccountsHandler(IUserAccountQueries userAccountQueries)
        {
            this.userAccountQueries = userAccountQueries;
        }

        public async Task<IEnumerable<QueryAccountResponse>> Handle(QueryAccountsRequest request, CancellationToken cancellationToken)
        {
            var accountsUser = await userAccountQueries.ListAccountsAsync(cancellationToken);
            return accountsUser;
        }
    }
}
