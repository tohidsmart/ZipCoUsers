using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;


namespace UserManaging.CQRS.Queries
{
    public class QueryUsersHandler : IRequestHandler<QueryUsersRequest, IEnumerable<QueryUserResponse>>
    {
        private readonly IUserAccountQueries userAccountQueries;
        public QueryUsersHandler(IUserAccountQueries userAccountQueries)
        {
            this.userAccountQueries = userAccountQueries;
        }
        public async Task<IEnumerable<QueryUserResponse>> Handle(QueryUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await userAccountQueries.ListUsersAsync(cancellationToken);
            return users;
        }
    }

}
