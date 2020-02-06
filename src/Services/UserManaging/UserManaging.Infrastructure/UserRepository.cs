using System;
using UserManaging.Domain;
using UserManaging.Domain.Repository;

namespace UserManaging.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagingContext context;

        public UserRepository(UserManagingContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return context;
            }
        }

        public User AddUser(User user)
        {
            return context.Users.Add(user).Entity;
        }

        public Account AddAccount(Account account)
        {
            return context.Accounts.Add(account).Entity;
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
