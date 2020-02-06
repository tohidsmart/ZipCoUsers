using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace UserManaging.CQRS.Queries
{
    public interface IUserAccountQueries
    {
        Task<IEnumerable<QueryUserResponse>> ListUsersAsync(CancellationToken token);
        Task<QueryUserResponse> GetUserAsync(Guid userId, CancellationToken token);
        Task<IEnumerable<QueryAccountResponse>> ListAccountsAsync(CancellationToken token);
        Task<QueryAccountResponse> GetAccountAsync(Guid acccountId, CancellationToken token);

    }
}
