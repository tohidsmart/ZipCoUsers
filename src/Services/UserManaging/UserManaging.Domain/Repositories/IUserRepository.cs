namespace UserManaging.Domain.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User AddUser(User user);
        void Update(User user);
        Account AddAccount(Account account);

    }
}
