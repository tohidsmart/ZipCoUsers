using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UserManaging.CQRS.Queries
{
    public class QueryUserHandler : IRequestHandler<QueryUserRequest, QueryUserResponse>
    {
        private readonly IUserAccountQueries userAccountQueries;
        public QueryUserHandler(IUserAccountQueries userAccountQueries)
        {
            this.userAccountQueries = userAccountQueries;
        }

        public async Task<QueryUserResponse> Handle(QueryUserRequest request, CancellationToken cancellationToken)
        {
            var userAccount = await userAccountQueries.GetUserAsync(request.UserId, cancellationToken);
            return userAccount;
        }
    }
}
