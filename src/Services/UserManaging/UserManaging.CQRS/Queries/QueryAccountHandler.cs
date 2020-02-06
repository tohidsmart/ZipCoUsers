using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UserManaging.CQRS.Queries
{
    public class QueryAccountHandler : IRequestHandler<QueryAccountRequest, QueryAccountResponse>
    {
        private readonly IUserAccountQueries userAccountQueries;

        public QueryAccountHandler(IUserAccountQueries userAccountQueries)
        {
            this.userAccountQueries = userAccountQueries;
        }
        public async Task<QueryAccountResponse> Handle(QueryAccountRequest request, CancellationToken cancellationToken)
        {
            var accountUser=await userAccountQueries.GetAccountAsync(request.AccountId, cancellationToken);
            return accountUser;
        }
    }
}
