using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManaging.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq;
using UserManaging.Domain;
using UserManaging.CQRS.Exceptions;

namespace UserManaging.CQRS.Queries
{

    public class UserAccountQueries : IUserAccountQueries
    {
        private readonly UserManagingContext context;
        private readonly IMapper mapper;
        public UserAccountQueries(UserManagingContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper;
        }

        public async Task<QueryAccountResponse> GetAccountAsync(Guid accountId, CancellationToken token)
        {
            var account = await context.Accounts.Include(acc => acc.User)
                            .FirstOrDefaultAsync(acc => acc.AccountId == accountId, token);
            if (account == null)
                throw new NotFoundException(nameof(account), accountId.ToString());
            var accountResponse = mapper.Map<Account, QueryAccountResponse>(account);
            return accountResponse;
        }

        public async Task<QueryUserResponse> GetUserAsync(Guid userId, CancellationToken token)
        {
            var user = await context.Users.Include(user => user.Account).
                FirstOrDefaultAsync(user => user.UserId == userId, token);
            if (user == null)
                throw new NotFoundException(nameof(user), userId.ToString());
            var userResponse = mapper.Map<User, QueryUserResponse>(user);
            return userResponse;
        }

        public async Task<IEnumerable<QueryAccountResponse>> ListAccountsAsync(CancellationToken token)
        {
            var accounts = await context.Accounts.Include(acc => acc.User)
                            .ToListAsync(token);
            var accountsResponse = accounts.Select(account => mapper.Map<Account, QueryAccountResponse>(account));
            return accountsResponse;
        }

        public async Task<IEnumerable<QueryUserResponse>> ListUsersAsync(CancellationToken token)
        {
            var users = await context.Users.Include(user => user.Account).ToListAsync(token);
            var usersResponse = users.Select(user => mapper.Map<User, QueryUserResponse>(user));
            return usersResponse;
        }
    }
}
