using Microsoft.EntityFrameworkCore;
using UserManaging.Domain;
using UserManaging.Domain.Repository;

namespace UserManaging.Infrastructure
{
    public class UserManagingContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public UserManagingContext(DbContextOptions<UserManagingContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagingContext).Assembly);
        }
    }
}
